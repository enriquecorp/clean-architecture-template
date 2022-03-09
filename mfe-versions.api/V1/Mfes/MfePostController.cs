using Mfes.Application.Create;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;

namespace mfe_versions.api.V1.Mfes
{
    [Tags("Mfes")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfes")]
    public class MfePostController : ApiBaseController
    {
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Index([FromBody] MfeRequest mfe)
        {
            Console.WriteLine(mfe);
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}
