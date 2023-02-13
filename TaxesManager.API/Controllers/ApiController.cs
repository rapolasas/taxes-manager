using Microsoft.AspNetCore.Mvc;

namespace TaxesManager.API.Controllers
{
    [Route("")]
    [Produces("application/json")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {

    }
}
