using MediatR;
using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Application.Queries.GetTaxByManicipality
{
    public class GetTaxByMunicipalityQueryHandler : IRequestHandler<GetTaxByMunicipalityQuery, GetTaxByMunicipalityQueryResult>
    {
        private readonly IMunicipalitiesRepository _municipalitiesRepository;

        public GetTaxByMunicipalityQueryHandler(IMunicipalitiesRepository municipalitiesRepository)
        {
            _municipalitiesRepository = municipalitiesRepository;
        }

        public async Task<GetTaxByMunicipalityQueryResult> Handle(GetTaxByMunicipalityQuery request, CancellationToken cancellationToken)
        {
            var municipality = await _municipalitiesRepository.GetAsync(request.MunicipalityId, cancellationToken);

            var appliedTax = municipality.FindTax(request.Date);

            return new GetTaxByMunicipalityQueryResult(appliedTax.Amount);
        }
    }
}
