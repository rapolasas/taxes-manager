using MediatR;
using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Application.Commands.CreateMunicipality
{
    public class CreateMunicipalityCommandHandler : IRequestHandler<CreateMunicipalityCommand, Municipality>
    {
        private readonly IMunicipalitiesRepository _municipalitiesRepository;

        public CreateMunicipalityCommandHandler(IMunicipalitiesRepository municipalitiesRepository)
        {
            _municipalitiesRepository = municipalitiesRepository;
        }

        public async Task<Municipality> Handle(CreateMunicipalityCommand request, CancellationToken cancellationToken)
        {
            var municipality = new Municipality(request.Name);
            _municipalitiesRepository.Add(municipality, cancellationToken);

            await _municipalitiesRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return municipality;
        }
    }
}
