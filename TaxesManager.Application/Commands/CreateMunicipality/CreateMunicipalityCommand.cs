using MediatR;
using TaxesManager.Domain.Municipalities;

namespace TaxesManager.Application.Commands.CreateMunicipality
{
    public record CreateMunicipalityCommand(string Name) : IRequest<Municipality>;
}
