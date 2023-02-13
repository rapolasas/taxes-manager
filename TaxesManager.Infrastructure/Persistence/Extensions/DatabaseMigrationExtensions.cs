using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TaxesManager.Infrastructure.Persistence.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<TaxesManagementDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
