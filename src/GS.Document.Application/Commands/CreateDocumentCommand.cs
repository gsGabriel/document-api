using GS.Document.Application.Framework.CommandHandler;
using GS.Document.Application.Resources;
using GS.Document.Domain.Entities.Validators;
using System;
using System.IO;
using System.Linq;

namespace GS.Document.Application.Commands
{
    public class CreateDocumentCommand : ICommand
    {
        public CreateDocumentCommand(Guid customerId, string fileName, string contentType, Stream fileStream)
        {
            CustomerId = customerId;
            FileName = fileName;
            ContentType = contentType;
            FileStream = fileStream;

            var validator = new CreateDocumentCommandValidator();
            var results = validator.Validate(this);

            if (!results.IsValid)
            {
                throw new ArgumentException(string.Format(DocumentApplicationResource.ValidationFailure, string.Join(", ", results.Errors.Select(x => x.ErrorMessage))));
            }
        }

        public Guid CustomerId { get; }
        public string FileName { get; }
        public string ContentType { get; }
        public Stream FileStream { get; }
    }
}
