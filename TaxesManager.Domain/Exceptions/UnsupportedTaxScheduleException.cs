using TaxesManager.Domain.Taxes;

namespace TaxesManager.Domain.Exceptions
{
    public class UnsupportedTaxScheduleException : Exception
    {
        public UnsupportedTaxScheduleException() : base($"Provided {nameof(TaxSchedule)} is unsupported")
        {

        }
    }
}
