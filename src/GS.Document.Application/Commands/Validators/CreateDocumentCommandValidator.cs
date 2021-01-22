using FluentValidation;
using GS.Document.Application.Commands;
using GS.Document.Application.Resources;

namespace GS.Document.Domain.Entities.Validators
{
    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.CustomerId).NotEmpty();
                })
                .WithMessage(DocumentApplicationResource.CustomerIdRequired);

            RuleFor(x => x.FileName).NotEmpty().WithMessage(DocumentApplicationResource.FileNameRequired);
            RuleFor(x => x.ContentType).NotEmpty().WithMessage(DocumentApplicationResource.ContentTypeRequired);
        }
    }
}
