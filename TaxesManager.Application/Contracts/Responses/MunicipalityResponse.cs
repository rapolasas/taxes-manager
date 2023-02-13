using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Application.Contracts.Responses
{
    public class MunicipalityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public static MunicipalityResponse FromDomain(Municipality municipality)
        {
            return new MunicipalityResponse
            {
                Id = municipality.Id,
                Name = municipality.Name,
            };
        }
    }
}
