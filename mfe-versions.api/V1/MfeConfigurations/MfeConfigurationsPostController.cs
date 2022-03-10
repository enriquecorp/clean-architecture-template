using mfe_versions.api.Constants;
using MfeConfigurations.Application.Create;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-configurations")]
    public class MfeConfigurationsPostController : ApiBaseController
    {
        private readonly MfeConfigurationCreator configurationCreator; //= new();

        public MfeConfigurationsPostController(MfeConfigurationCreator configurationCreator)
        {
            this.configurationCreator = configurationCreator;
        }
        // GET: api/<MfeConfigurationsPostController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MfeConfigurationsPostController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<MfeConfigurationsPostController>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromHeader(Name = ApiHeaders.TENANT_ID)] string tenantId, [FromBody] MfeConfigurationRequest mfeConfiguration)
        {
            Console.WriteLine($"TenantId = {tenantId}");
            Console.WriteLine($"Configurations Length = {mfeConfiguration.Configurations.Count}");
            mfeConfiguration.TenantId = tenantId;
            this.configurationCreator.Execute(mfeConfiguration);
            return this.StatusCode(StatusCodes.Status201Created);
        }

        //// PUT api/<MfeConfigurationsPostController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MfeConfigurationsPostController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
