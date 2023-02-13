using MediatR;

namespace TaxesManager.Application.Queries.GetTaxByManicipality
{
    public class GetTaxByMunicipalityQuery : IRequest<GetTaxByMunicipalityQueryResult>
    {
        public Guid MunicipalityId { get; set; }
        public DateTime Date { get; set; }

        public GetTaxByMunicipalityQuery(Guid municipalityId, DateTime date)
        {
            MunicipalityId = municipalityId;
            Date = date;
        }
    }
}
