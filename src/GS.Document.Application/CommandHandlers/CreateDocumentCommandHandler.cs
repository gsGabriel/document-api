using GS.Document.Application.Commands;
using GS.Document.Application.Framework.CommandHandler;
using GS.Document.Domain.Repositories;
using GS.Document.Domain.ValueObjects;
using GS.Document.Infra.Kafka.Interfaces;
using GS.Document.Infra.S3.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.Application.CommandHandlers
{
    public class CreateDocumentCommandHandler : ICommandHandler<CreateDocumentCommand, DocumentId>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IBucketService bucketService;
        private readonly IEventProducer eventProducer;
        private readonly ILogger<CreateDocumentCommandHandler> logger;

        public CreateDocumentCommandHandler(ICustomerRepository customerRepository, IBucketService bucketService, IEventProducer eventProducer, ILogger<CreateDocumentCommandHandler> logger)
        {
            this.customerRepository = customerRepository ?? throw new System.ArgumentNullException(nameof(customerRepository));
            this.bucketService = bucketService ?? throw new System.ArgumentNullException(nameof(bucketService));
            this.eventProducer = eventProducer ?? throw new System.ArgumentNullException(nameof(eventProducer));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<DocumentId> HandleAsync(CreateDocumentCommand command, CancellationToken cancellationToken = default)
        {
            logger.LogInformation($"Create new document to customer {command.CustomerId} has started.");

            var customer = await customerRepository.FindOrCreateAsync(command.CustomerId);
            var path = await bucketService.UploadAsync(command.FileStream);

            var documentId = customer.AddDocument(command.FileName, command.ContentType, path);

            await customerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await eventProducer.ProduceAsync("customer-document-created", customer.DomainEvents);

            logger.LogInformation($"Create new document to customer {command.CustomerId} was completed.");

            return documentId;
        }
    }
}
