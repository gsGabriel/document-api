using GS.Document.Domain.Aggregates.Validators;
using GS.Document.Domain.Entities;
using GS.Document.Domain.Events;
using GS.Document.Domain.Framework.Aggregates;
using GS.Document.Domain.Resources;
using GS.Document.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Document.Domain.Aggregates
{
    public class CustomerAggregate : AggregateRoot
    {
        protected CustomerAggregate()
            : base()
        {
            _documents = new List<Documents>();
        }

        private CustomerAggregate(Guid customerId)
            : this()
        {
            Id = customerId;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime CreatedAt { get; }

        private readonly List<Documents> _documents;
        public IReadOnlyCollection<Documents> Documents => _documents.AsReadOnly();

        /// <summary>
        /// Create new customers aggregate
        /// </summary>
        /// <param name="customerId">Customer Identifier</param>
        /// <returns>Instances of customer aggregate</returns>
        public static CustomerAggregate CreateFrom(Guid customerId)
        {
            var customer = new CustomerAggregate(customerId);
            var validator = new CustomerAggregateValidator();
            var results = validator.Validate(customer);

            if (!results.IsValid)
            {
                throw new ArgumentException(string.Format(DocumentDomainResource.ValidationFailure, string.Join(", ", results.Errors.Select(x => x.ErrorMessage))));
            }
            return customer;
        }

        /// <summary>
        /// Add new document to a customer
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="contentType">Content Type</param>
        /// <param name="path">Path for file</param>
        public DocumentId AddDocument(string fileName, string contentType, string path)
        {
            if (Documents.Any(x => x.FileName == fileName))
                throw new InvalidOperationException(DocumentDomainResource.ExistingDocument);

            var document = Entities.Documents.CreateFrom(CustomerId.From(Id), fileName, contentType, path);
            _documents.Add(document);

            Raise(DocumentCreated.CreateFrom(document));

            return document.Id;
        }
    }
}
