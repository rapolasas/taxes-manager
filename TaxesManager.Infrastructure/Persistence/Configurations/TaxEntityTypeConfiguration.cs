using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Infrastructure.Persistence.Configurations
{
    internal class TaxEntityTypeConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder
                .Property(p => p.Schedule)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder
                .Property(p => p.StartDate)
                .HasColumnType("Date");

            builder
                .Property(p => p.EndDate)
                .HasColumnType("Date");

            builder
                .Property(p => p.Amount)
                .HasColumnType("Decimal(3, 2)");
        }
    }
}
