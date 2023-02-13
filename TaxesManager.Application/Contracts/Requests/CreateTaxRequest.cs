using System.ComponentModel.DataAnnotations;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Contracts.Requests
{
    public class CreateTaxRequest
    {
        [Required]
        public Guid MunicipalityId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public TaxSchedule Schedule { get; set; }
    }
}
