using TaxesManager.Domain.Common;

namespace TaxesManager.Domain.Municipalities
{
    public interface IMunicipalitiesRepository : IRepository
    {
        Task<Municipality> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Municipality Add(Municipality municipality, CancellationToken cancellationToken = default);
        void Update(Municipality municipality, CancellationToken cancellationToken = default);
    }
}
