using FluentValidation;

namespace TaxesManager.Application.Commands.CreateMunicipality
{
    public class CreateMunicipalityCommandValidator : AbstractValidator<CreateMunicipalityCommand>
    {
        public CreateMunicipalityCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
