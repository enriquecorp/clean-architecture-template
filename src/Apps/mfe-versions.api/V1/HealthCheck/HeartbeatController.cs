using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.HealthCheck
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HeartbeatController : Controller
    {
        public HeartbeatController()
        {
        }
        // GET: api/v1/heartbeat/ping
        [HttpGet]
        [Route("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Get()
        {
            // return await Task.FromResult<ActionResult<bool>>(Ok(true));
            return await Task.Run(() => this.Ok(true));
            //return Ok(true);
        }
    }
}
