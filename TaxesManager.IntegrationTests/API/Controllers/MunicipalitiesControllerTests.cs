using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using TaxesManager.Application.Contracts.Requests;
using TaxesManager.Application.Contracts.Responses;
using TaxesManager.Infrastructure.Persistence;

namespace TaxesManager.IntegrationTests.API.Controllers
{
    internal class MunicipalitiesControllerTests : TestsBase
    {
        private const string Route = "api/municipalities";

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();
            await dbContext.Taxes.BatchDeleteAsync();
            await dbContext.Municipalities.BatchDeleteAsync();
        }

        [Test]
        public async Task CreateMunicipality_ShouldReturnCreatedMunicipality()
        {
            // Arrange
            var createMunicipalityRequest = new CreateMunicipalityRequest
            {
                Name = "Vilnius"
            };

            // Act
            var response = await InvokeHttp(createMunicipalityRequest, request => ApplicationClient.PostAsync(Route, request));
            var municipalityResponse = JsonConvert.DeserializeObject<MunicipalityResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.NotNull(municipalityResponse);
            Assert.AreEqual(createMunicipalityRequest.Name, municipalityResponse.Name);
        }

        [Test]
        public async Task CreateMunicipality_ShouldCreateRecordInDatabase()
        {
            // Arrange
            var createMunicipalityRequest = new CreateMunicipalityRequest
            {
                Name = "Kaunas"
            };

            // Act
            var response = await InvokeHttp(createMunicipalityRequest, request => ApplicationClient.PostAsync(Route, request));
            var municipalityResponse = JsonConvert.DeserializeObject<MunicipalityResponse>(await response.Content.ReadAsStringAsync());

            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();

            var isCreated = await dbContext.Municipalities.AnyAsync(e => e.Id == municipalityResponse.Id);

            // Assert
            Assert.IsTrue(isCreated);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();
            await dbContext.Municipalities.BatchDeleteAsync();
        }
    }
}
