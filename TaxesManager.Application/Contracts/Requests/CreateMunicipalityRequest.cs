using System.ComponentModel.DataAnnotations;

namespace TaxesManager.Application.Contracts.Requests
{
    public class CreateMunicipalityRequest
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
