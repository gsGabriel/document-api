using FluentValidation;
using GS.Document.Domain.Resources;

namespace GS.Document.Domain.Aggregates.Validators
{
    public class CustomerAggregateValidator : AbstractValidator<CustomerAggregate>
    {
        public CustomerAggregateValidator()
        {
            RuleFor(x => x.Id).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Id).NotEmpty();
                })
                .WithMessage(DocumentDomainResource.CustomerIdRequired);
        }
    }
}
