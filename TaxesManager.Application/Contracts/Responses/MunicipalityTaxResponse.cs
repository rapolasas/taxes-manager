using TaxesManager.Application.Queries.GetTaxByManicipality;

namespace TaxesManager.Application.Contracts.Responses
{
    public class MunicipalityTaxResponse
    {
        public decimal Amount { get; set; }

        public static MunicipalityTaxResponse FromQueryResult(GetTaxByMunicipalityQueryResult result)
        {
            return new MunicipalityTaxResponse
            {
                Amount = result.Amount
            };
        }
    }
}
