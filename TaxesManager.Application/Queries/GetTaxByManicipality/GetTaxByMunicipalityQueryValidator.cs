using FluentValidation;

namespace TaxesManager.Application.Queries.GetTaxByManicipality
{
    public class GetTaxByMunicipalityQueryValidator : AbstractValidator<GetTaxByMunicipalityQuery>
    {
        public GetTaxByMunicipalityQueryValidator()
        {
            RuleFor(r => r.MunicipalityId)
                .NotNull()
                .NotEmpty();
        }
    }
}
