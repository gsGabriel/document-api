using GS.Document.Domain.Entities;
using GS.Document.Domain.Framework.Events;
using GS.Document.Domain.ValueObjects;

namespace GS.Document.Domain.Events
{
    public class DocumentCreated : DomainEvent
    {
        private DocumentCreated(CustomerId customerId, string fileName, string contentType, string path)
        {
            CustomerId = customerId;
            FileName = fileName;
            ContentType = contentType;
            Path = path;
        }

        public CustomerId CustomerId { get; }
        public string FileName { get; }
        public string ContentType { get; }
        public string Path { get; }

        public static DocumentCreated CreateFrom(Entities.Documents document)
        {
            return new DocumentCreated(document.CustomerId, document.FileName, document.ContentType, document.Path);
        }
    }
}
