using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using TaxesManager.Application.Contracts.Filters;
using TaxesManager.Application.Contracts.Requests;
using TaxesManager.Application.Contracts.Responses;
using TaxesManager.Domain.Municipalities;
using TaxesManager.Domain.Taxes;
using TaxesManager.Infrastructure.Persistence;

namespace TaxesManager.IntegrationTests.API.Controllers
{
    internal class TaxesControllerTests : TestsBase
    {
        private const string Route = "api/taxes";
        private const string FromQuery = Route + "?MunicipalityId={0}&Date={1}";
        private Guid _copenhagenMunicipalityId;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();
            await dbContext.Taxes.BatchDeleteAsync();
            await dbContext.Municipalities.BatchDeleteAsync();

            var municipality = dbContext.Municipalities.Add(new Municipality("Copenhagen")).Entity;
            _copenhagenMunicipalityId = municipality.Id;

            var yearlyTax = municipality.AddTax(0.2M, DateTime.Parse("2023-01-01"), TaxSchedule.Yearly);
            yearlyTax.EndDate = DateTime.Parse("2023-12-31");

            var monthlyTax = municipality.AddTax(0.4M, DateTime.Parse("2023-05-01"), TaxSchedule.Monthly);
            monthlyTax.EndDate = DateTime.Parse("2023-05-31");

            var dailyTaxFirst = municipality.AddTax(0.1M, DateTime.Parse("2023-01-01"), TaxSchedule.Daily);
            dailyTaxFirst.EndDate = DateTime.Parse("2023-01-01");

            var dailyTaxSecond = municipality.AddTax(0.1M, DateTime.Parse("2023-12-25"), TaxSchedule.Daily);
            dailyTaxSecond.EndDate = DateTime.Parse("2023-12-25");

            await dbContext.SaveChangesAsync();
        }

        [Test]
        public async Task CreateTax_ShouldReturnCreatedTax()
        {
            // Arrange
            var createTaxRequest = new CreateTaxRequest
            {
                Amount = 0.3M,
                StartDate = DateTime.Parse("2023-02-01"),
                Schedule = TaxSchedule.Weekly,
                MunicipalityId = _copenhagenMunicipalityId
            };

            // Act
            var response = await InvokeHttp(createTaxRequest, request => ApplicationClient.PostAsync(Route, request));
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.NotNull(taxResponse);
            Assert.AreEqual(createTaxRequest.Amount, taxResponse.Amount);
            Assert.AreEqual(createTaxRequest.StartDate, taxResponse.StartDate);            
            Assert.AreEqual(createTaxRequest.Schedule, taxResponse.Schedule);
            Assert.AreEqual(createTaxRequest.StartDate.AddDays(7), taxResponse.EndDate);
        }

        [Test]
        public async Task CreateTax_ShouldCreateRecordInDatabase()
        {
            // Arrange
            var createTaxRequest = new CreateTaxRequest
            {
                Amount = 0.3M,
                StartDate = DateTime.Parse("2023-03-01"),
                Schedule = TaxSchedule.Daily,
                MunicipalityId = _copenhagenMunicipalityId
            };

            // Act
            var response = await InvokeHttp(createTaxRequest, request => ApplicationClient.PostAsync(Route, request));
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(await response.Content.ReadAsStringAsync());

            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();

            var isCreated = await dbContext.Taxes.AnyAsync(e => e.Id == taxResponse.Id);

            // Assert
            Assert.IsTrue(isCreated);
        }

        [Test]
        public async Task CreateTax_ShouldReturnNotFound_WhenMunicipalityDoesNotExist()
        {
            // Arrange
            var createTaxRequest = new CreateTaxRequest
            {
                Amount = 0.3M,
                StartDate = DateTime.Parse("2023-03-01"),
                Schedule = TaxSchedule.Daily,
                MunicipalityId = Guid.NewGuid()
            };

            // Act
            var response = await InvokeHttp(createTaxRequest, request => ApplicationClient.PostAsync(Route, request));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task CreateTax_ShouldReturnBadRequest_WhenDuplicateTaxExists()
        {
            // Arrange
            var createTaxRequest = new CreateTaxRequest
            {
                Amount = 0.3M,
                StartDate = DateTime.Parse("2023-01-01"),
                Schedule = TaxSchedule.Daily,
                MunicipalityId = _copenhagenMunicipalityId
            };

            // Act
            var response = await InvokeHttp(createTaxRequest, request => ApplicationClient.PostAsync(Route, request));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task CreateTax_ShouldReturnBadRequest_WhenTaxScheduleDoesNotExist()
        {
            // Arrange
            var createTaxRequest = new CreateTaxRequest
            {
                Amount = 0.3M,
                StartDate = DateTime.Parse("2023-01-01"),
                Schedule = (TaxSchedule)99,
                MunicipalityId = _copenhagenMunicipalityId
            };

            // Act
            var response = await InvokeHttp(createTaxRequest, request => ApplicationClient.PostAsync(Route, request));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetTax_ShouldReturnNotFound_WhenMunicipalityDoesNotExist()
        {
            // Arrange
            var getTaxFilter = new GetTaxFilter
            {
                MunicipalityId = Guid.NewGuid()
            };

            // Act
            var response = await InvokeHttp(getTaxFilter, request => ApplicationClient.GetAsync(Route));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetTax_ShouldReturnNotFound_WhenTaxDoesNotExist()
        {
            // Arrange
            var getTaxFilter = new GetTaxFilter
            {
                MunicipalityId = Guid.NewGuid(),
                Date = DateTime.Parse("2022-01-01")
            };

            // Act
            var response = await InvokeHttp(getTaxFilter, request => ApplicationClient.GetAsync(Route));

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task GetTax_ShouldReturnDailyTax_WhenDailyAndYearlyTaxExists()
        {
            // Arrange
            var getTaxFilter = new GetTaxFilter
            {
                MunicipalityId = _copenhagenMunicipalityId,
                Date = DateTime.Parse("2023-01-01")
            };

            // Act
            var response = await ApplicationClient.GetAsync(string.Format(FromQuery, getTaxFilter.MunicipalityId, getTaxFilter.Date));
            var taxResponse = JsonConvert.DeserializeObject<MunicipalityTaxResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(0.1M, taxResponse.Amount);
        }

        [Test]
        public async Task GetTax_ShouldReturnMonthlyTax_WhenMonthlyAndYearlyTaxExists()
        {
            // Arrange
            var getTaxFilter = new GetTaxFilter
            {
                MunicipalityId = _copenhagenMunicipalityId,
                Date = DateTime.Parse("2023-05-01")
            };

            // Act
            var response = await ApplicationClient.GetAsync(string.Format(FromQuery, getTaxFilter.MunicipalityId, getTaxFilter.Date));
            var taxResponse = JsonConvert.DeserializeObject<MunicipalityTaxResponse>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(0.4M, taxResponse.Amount);
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaxesManagementDbContext>();
            await dbContext.Taxes.BatchDeleteAsync();
        }
    }
}
