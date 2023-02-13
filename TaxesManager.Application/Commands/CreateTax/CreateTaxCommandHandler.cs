using MediatR;
using TaxesManager.Domain.Municipalities;
using TaxesManager.Domain.Taxes;

namespace TaxesManager.Application.Commands.CreateTax
{
    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, Tax>
    {
        private readonly IMunicipalitiesRepository _municipalitiesRepository;

        public CreateTaxCommandHandler(IMunicipalitiesRepository municipalitiesRepository)
        {
            _municipalitiesRepository = municipalitiesRepository;
        }

        public async Task<Tax> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var municipality = await _municipalitiesRepository.GetAsync(request.MunicipalityId, cancellationToken);

            var newTax = municipality.AddTax(request.Amount, request.StartDate, request.Schedule);
            _municipalitiesRepository.Update(municipality, cancellationToken);

            await _municipalitiesRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return newTax;
        }
    }
}
