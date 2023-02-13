using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxesManager.Application.Commands.CreateMunicipality;
using TaxesManager.Application.Contracts.Requests;
using TaxesManager.Application.Contracts.Responses;

namespace TaxesManager.API.Controllers
{
    public class MunicipalitiesController : ApiController
    {
        private readonly IMediator _mediator;

        public MunicipalitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/municipalities")]
        public async Task<ActionResult<MunicipalityResponse>> Post([FromBody] CreateMunicipalityRequest request)
        {
            var command = new CreateMunicipalityCommand(request.Name);
            var municipality = await _mediator.Send(command);

            return Ok(MunicipalityResponse.FromDomain(municipality));
        }
    }
}
