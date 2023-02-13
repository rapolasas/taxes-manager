using Microsoft.EntityFrameworkCore;
using TaxesManager.Domain.Common;
using TaxesManager.Domain.Exceptions;
using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Infrastructure.Persistence.Repositories
{
    internal class MunicipalitiesRepository : IMunicipalitiesRepository
    {
        private readonly TaxesManagementDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public MunicipalitiesRepository(TaxesManagementDbContext context)
        {
            _context = context;
        }

        public Municipality Add(Municipality municipality, CancellationToken cancellationToken)
        {
            return _context.Municipalities.Add(municipality).Entity;
        }

        public async Task<Municipality> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var municipality = await _context.Municipalities
                .Include(p => p.Taxes)
                .AsSplitQuery()
                .SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

            if(municipality is null)
            {
                throw new NotFoundException(nameof(Municipality));
            }
            return municipality;
        }

        public void Update(Municipality municipality, CancellationToken cancellationToken)
        {
            _context.Entry(municipality).State = EntityState.Modified;
        }
    }
}
