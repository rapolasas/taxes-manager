using System.ComponentModel.DataAnnotations;

namespace TaxesManager.Application.Contracts.Filters
{
    public class GetTaxFilter
    {
        [Required]
        public Guid MunicipalityId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
