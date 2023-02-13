using TaxesManager.Application.Common.Interfaces;

namespace TaxesManager.Infrastructure.Services
{
    internal class DateTimeProviderService : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
