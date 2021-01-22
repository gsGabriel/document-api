using GS.Document.Domain.Entities.Validators;
using GS.Document.Domain.Resources;
using GS.Document.Domain.ValueObjects;
using System;
using System.Linq;

namespace GS.Document.Domain.Entities
{
    public class Documents
    {
        protected Documents()
        { }

        private Documents(CustomerId customerId, string fileName, string contentType, string path)
        {
            _id = Guid.NewGuid();
            _customerId = customerId.Value;
            FileName = fileName;
            ContentType = contentType;
            Path = path;
            CreatedAt = DateTimeOffset.Now;
        }

        private readonly Guid _id;
        private readonly Guid _customerId;

        public DocumentId Id => DocumentId.From(_id);
        public CustomerId CustomerId => CustomerId.From(_customerId);
        public string FileName { get; }
        public string ContentType { get; }
        public string Path { get; }
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Create new document instance
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="fileName">File Name</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="path">Path for file</param>
        /// <returns>Instances for new document</returns>
        public static Documents CreateFrom(CustomerId customerId, string fileName, string contentType, string path)
        {
            var document = new Documents(customerId, fileName, contentType, path);
            var validator = new DocumentValidator();
            var results = validator.Validate(document);

            if (!results.IsValid)
            {
                throw new ArgumentException(string.Format(DocumentDomainResource.ValidationFailure, string.Join(", ", results.Errors.Select(x => x.ErrorMessage))));
            }

            if (!document.ContentType.ToLower().Contains("application/pdf"))
                throw new InvalidOperationException(DocumentDomainResource.ContentTypeNotAllowed);

            return document;
        }
    }
}
