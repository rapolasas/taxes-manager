using TaxesManager.Domain.Exceptions;
using TaxesManager.Domain.Municipalities;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.UnitTests.Domain
{
    [TestFixture]
    public class MunicipalityTests
    {
        [Test]
        public void AddTax_ShouldAddTax()
        {
            // Arrange
            var mun = new Municipality("Test");
            var startDate = DateTime.UtcNow;
            var amount = 0.5M;
            var schedule = TaxSchedule.Monthly;

            // Act
            var tax = mun.AddTax(amount, startDate, schedule);

            // Assert
            Assert.AreEqual(amount, tax.Amount);
            Assert.AreEqual(startDate, tax.StartDate);
            Assert.AreEqual(schedule, tax.Schedule);
            Assert.AreEqual(1, mun.Taxes.Count);
        }

        [Test]
        public void AddTax_ShouldThrowDuplicateException_WhenTaxAlreadyExists()
        {
            // Arrange
            var municipality = new Municipality("Test");
            var amount = 0.5M;
            var startDate = DateTime.UtcNow;
            var schedule = TaxSchedule.Daily;
            municipality.AddTax(amount, startDate, schedule);

            // Act & Assert
            Assert.Throws<DuplicateException>(() => municipality.AddTax(amount, startDate, schedule));
        }

        [Test]
        public void AddTax_Should_Throw_UnsupportedTaxScheduleException_When_Invalid_Schedule_Is_Provided()
        {
            // Arrange
            var municipality = new Municipality("Test");

            // Act & Assert
            Assert.Throws<UnsupportedTaxScheduleException>(() => municipality.AddTax(0.5M, DateTime.UtcNow, (TaxSchedule)99));
        }
    }
}