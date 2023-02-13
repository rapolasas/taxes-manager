using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Contracts.Responses
{
    public class TaxResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaxSchedule Schedule { get; set; }

        public static TaxResponse FromDomain(Tax tax)
        {
            return new TaxResponse
            {
                Id = tax.Id,
                Amount = tax.Amount,
                Schedule = tax.Schedule,
                StartDate = tax.StartDate,
                EndDate = tax.EndDate
            };
        }
    }
}
