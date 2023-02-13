using MediatR;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Commands.CreateTax
{
    public class CreateTaxCommand : IRequest<Tax>
    {
        public Guid MunicipalityId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public TaxSchedule Schedule { get; set; }

        public CreateTaxCommand(DateTime startDate, Guid municipalityId, decimal amount, TaxSchedule schedule)
        {
            StartDate = startDate;
            MunicipalityId = municipalityId;
            Amount = amount;
            Schedule = schedule;
        }
    }
}
