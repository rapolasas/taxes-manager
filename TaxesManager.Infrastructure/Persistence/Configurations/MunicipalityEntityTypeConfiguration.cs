using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Infrastructure.Persistence.Configurations
{
    internal class MunicipalityEntityTypeConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(500);
        }
    }
}
