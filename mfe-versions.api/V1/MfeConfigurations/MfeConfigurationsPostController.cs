using System.Net;
using mfe_versions.api.Constants;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class MfeConfigurationsPostController : ApiBaseController
    {
        private readonly MfeTenantConfigurationCreator configurationCreator; //= new();

        public MfeConfigurationsPostController(MfeTenantConfigurationCreator configurationCreator)
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

        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName =nameof(MfeConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] MfeTenantConfigurationRequest mfeConfiguration)
        {
            await this.configurationCreator.Execute(mfeConfiguration);
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
