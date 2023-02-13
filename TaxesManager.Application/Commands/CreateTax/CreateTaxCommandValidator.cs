using FluentValidation;
using TaxesManager.Application.Common.Interfaces;

namespace TaxesManager.Application.Commands.CreateTax
{
    public class CreateTaxCommandValidator : AbstractValidator<CreateTaxCommand>
    {
        public CreateTaxCommandValidator(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(r => r.StartDate)
                .Must(date => date > dateTimeProvider.UtcNow.Date)
                .WithMessage("Start date cannot be equal or less than today");
        }
    }
}
