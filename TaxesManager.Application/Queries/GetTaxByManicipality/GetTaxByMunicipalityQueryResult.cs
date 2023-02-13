using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Queries.GetTaxByManicipality
{
    public class GetTaxByMunicipalityQueryResult
    {
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public TaxSchedule Schedule { get; set; }

        public GetTaxByMunicipalityQueryResult(decimal amount)
        {
            Amount = amount;
        }
    }
}
