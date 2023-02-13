using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxesManager.Application.Common.Interfaces;
using TaxesManager.Domain.Municipalities;
using TaxesManager.Infrastructure.Persistence;
using TaxesManager.Infrastructure.Persistence.Repositories;
using TaxesManager.Infrastructure.Services;

namespace TaxesManager.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddTaxesStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ApiTestsDatabase");

            return services
                .AddDbContext<TaxesManagementDbContext>(options => options.UseSqlServer(connectionString))
                .AddScoped<IMunicipalitiesRepository, MunicipalitiesRepository>();
        }

        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
        {
            return services.AddScoped<IDateTimeProvider, DateTimeProviderService>();
        }
    }
}