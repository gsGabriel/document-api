using GS.Document.Application.Commands;
using GS.Document.Application.Framework.CommandHandler;
using GS.Document.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Document.API.V1.Controllers
{
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/customers/")]
    public class DocumentController : ControllerBase
    {
        private readonly ICommandHandler<CreateDocumentCommand, DocumentId> createDocumentCommandHandler;

        public DocumentController(ICommandHandler<CreateDocumentCommand, DocumentId> createDocumentCommandHandler)
        {
            this.createDocumentCommandHandler = createDocumentCommandHandler;
        }

        [HttpPost("{customerId}/documents")]
        [SwaggerOperation(Description = "Realiza o upload de um novo documento para um usuário")]
        [SwaggerResponse(201, "Upload realizado")]
        public async Task<IActionResult> Post([FromRoute] Guid customerId, IFormFile file, CancellationToken cancellationToken)
        {
            using var stream = file.OpenReadStream();
            var command = new CreateDocumentCommand(customerId, file.FileName, file.ContentType, stream);
            var documentId = await createDocumentCommandHandler.HandleAsync(command, cancellationToken);

            return Created("", new { documentId = documentId.Value });
        }
    }
}
