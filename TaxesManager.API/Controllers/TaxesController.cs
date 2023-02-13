using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxesManager.Application.Commands.CreateTax;
using TaxesManager.Application.Contracts.Filters;
using TaxesManager.Application.Contracts.Requests;
using TaxesManager.Application.Contracts.Responses;
using TaxesManager.Application.Queries.GetTaxByManicipality;

namespace TaxesManager.API.Controllers
{
    public class TaxesController : ApiController
    {
        private readonly IMediator _mediator;

        public TaxesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/municipalities/{municipalityId:guid}/taxes")]
        public async Task<ActionResult<MunicipalityTaxResponse>> Get([FromRoute] Guid municipalityId, [FromQuery] GetTaxFilter taxFilter)
        {
            var query = new GetTaxByMunicipalityQuery(municipalityId, taxFilter.Date);
            var taxQueryResult = await _mediator.Send(query);

            return Ok(MunicipalityTaxResponse.FromQueryResult(taxQueryResult));
        }

        [HttpPost("api/municipalities/{municipalityId:guid}/taxes")]
        public async Task<ActionResult<TaxResponse>> Post([FromRoute] Guid municipalityId, [FromBody] CreateTaxRequest request)
        {
            var command = new CreateTaxCommand(request.StartDate, municipalityId, request.Amount, request.Schedule);
            var tax = await _mediator.Send(command);

            return Ok(TaxResponse.FromDomain(tax));
        }
    }
}
