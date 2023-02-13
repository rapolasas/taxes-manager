using System.ComponentModel.DataAnnotations;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Contracts.Requests
{
    public class CreateTaxRequest
    {
        [Required]
        [Range(0, 1)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public TaxSchedule Schedule { get; set; }
    }
}
