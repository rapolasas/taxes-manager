using TaxesManager.Domain.Common;
using TaxesManager.Domain.Exceptions;

namespace TaxesManager.Domain.Taxes
{
    public class Tax : Entity
    {
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaxSchedule Schedule { get; set; }

        public Tax(decimal amount, DateTime startDate, TaxSchedule schedule)
        {
            Amount = amount;
            StartDate = startDate;
            Schedule = schedule;
            CalculateEndDate();
        }

        public bool Exists(DateTime startDate, TaxSchedule schedule)
        {
            if(Schedule != schedule)
            {
                return false;
            }

            return schedule switch
            {
                TaxSchedule.Daily => startDate.Date == StartDate.Date,
                _ => startDate.Date >= StartDate.Date && startDate.Date <= EndDate.Date
            };           
        }

        private void CalculateEndDate()
        {
            EndDate = Schedule switch
            {
                TaxSchedule.Yearly => StartDate.AddYears(1),
                TaxSchedule.Monthly => StartDate.AddMonths(1),
                TaxSchedule.Weekly => StartDate.AddDays(7),
                TaxSchedule.Daily => StartDate,
                _ => throw new UnsupportedTaxScheduleException(),
            };
        }
    }
}
