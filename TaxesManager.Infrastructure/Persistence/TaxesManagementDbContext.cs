using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaxesManager.Domain.Common;
using TaxesManager.Domain.Municipalities;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Infrastructure.Persistence
{
    public class TaxesManagementDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Municipality> Municipalities => Set<Municipality>();
        public DbSet<Tax> Taxes => Set<Tax>();
        public TaxesManagementDbContext(DbContextOptions<TaxesManagementDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            SeedDatabase(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Not the best way to seed data but for time saving purposes it was chosen to it this way
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            var municipalityId = Guid.Parse("2B24D1AA-0761-4843-99B9-E3A93BC2561E");

            modelBuilder
                .Entity<Municipality>()
                .HasData(new Municipality("Copenhagen") { Id = municipalityId });

            modelBuilder.Entity<Tax>().HasData(
                new { Amount = 0.2M, StartDate = DateTime.Parse("2023-01-01"), Schedule = TaxSchedule.Yearly, EndDate = DateTime.Parse("2023-12-31"), Id = Guid.NewGuid(), MunicipalityId = municipalityId },
                new { Amount = 0.4M, StartDate = DateTime.Parse("2023-05-01"), Schedule = TaxSchedule.Monthly, EndDate = DateTime.Parse("2023-05-31"), Id = Guid.NewGuid(), MunicipalityId = municipalityId },
                new { Amount = 0.1M, StartDate = DateTime.Parse("2023-01-01"), Schedule = TaxSchedule.Daily, EndDate = DateTime.Parse("2023-01-01"), Id = Guid.NewGuid(), MunicipalityId = municipalityId },
                new { Amount = 0.1M, StartDate = DateTime.Parse("2023-12-25"), Schedule = TaxSchedule.Daily, EndDate = DateTime.Parse("2023-12-25"), Id = Guid.NewGuid(), MunicipalityId = municipalityId }
            );
        }
    }
}
