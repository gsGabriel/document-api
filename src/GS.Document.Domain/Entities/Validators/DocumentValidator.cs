using FluentValidation;
using GS.Document.Domain.Resources;

namespace GS.Document.Domain.Entities.Validators
{
    public class DocumentValidator : AbstractValidator<Documents>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.CustomerId).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.CustomerId).NotEmpty();
                })
                .WithMessage(DocumentDomainResource.CustomerIdRequired);

            RuleFor(x => x.FileName).NotEmpty().WithMessage(DocumentDomainResource.FileNameRequired);
            RuleFor(x => x.ContentType).NotEmpty().WithMessage(DocumentDomainResource.ContentTypeRequired);
            RuleFor(x => x.Path).NotEmpty().WithMessage(DocumentDomainResource.PathRequired);
        }
    }
}
