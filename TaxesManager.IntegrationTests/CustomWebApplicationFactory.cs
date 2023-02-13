using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaxesManager.Application.Common.Interfaces;

namespace TaxesManager.IntegrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        /// <summary>
        /// This is not fully implemented because of lack of time. Idea was to override the connection string that lives in appsettings.json in API project
        /// so that it was possible to run integration tests against another database. To make integrations tests work, 'ApiTestsDatabase' 
        /// connection string needs to be used instead of 'Database' so that migrations can be properly applied. This needs to be done in infrastructure layer
        /// ConfigureServices.cs
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //builder.ConfigureAppConfiguration(configurationBuilder =>
            //{
            //    var integrationConfig = new ConfigurationBuilder()
            //        .AddJsonFile("appsettings.test.json")
            //        .AddEnvironmentVariables()
            //        .Build();

            //    configurationBuilder.AddConfiguration(integrationConfig);
            //});

            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = new ServiceDescriptor(typeof(IDateTimeProvider), typeof(FixedDateTimeProvider), ServiceLifetime.Scoped);
                services.Replace(serviceDescriptor);
            });
        }
    }

    internal class FixedDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Parse("2023-01-15");
    }
}
