using MediatR;

namespace TaxesManager.Application.Queries.GetTaxByManicipality
{
    public record GetTaxByMunicipalityQuery(Guid MunicipalityId, DateTime Date) 
        : IRequest<GetTaxByMunicipalityQueryResult>;
}
