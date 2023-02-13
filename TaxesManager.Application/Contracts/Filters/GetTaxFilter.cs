using System.ComponentModel.DataAnnotations;

namespace TaxesManager.Application.Contracts.Filters
{
    public class GetTaxFilter
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
