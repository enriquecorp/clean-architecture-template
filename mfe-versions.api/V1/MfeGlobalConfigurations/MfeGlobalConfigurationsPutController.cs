using System.Net;
using MfeGlobalConfigurations.Application.Update;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeGlobalConfigurations
{
    [Tags("MfeGlobalConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-global-configurations")]
    public class MfeGlobalConfigurationsPutController : ApiBaseController
    {
        private readonly MfeGlobalConfigurationUpdator configurationUpdator; //= new();

        public MfeGlobalConfigurationsPutController(MfeGlobalConfigurationUpdator configurationUpdator)
        {
            this.configurationUpdator = configurationUpdator;
        }

        // POST api/v{version:apiVersion}/mfe-configurations
        //[TypeFilter(typeof(DomainExceptionFilter))]
        //[DomainExceptionMapper(ExceptionTypeName =nameof(MfeConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] MfeGlobalConfigurationRequest globalConfiguration)
        {
            this.configurationUpdator.Execute(new MfeId(globalConfiguration.MfeId), new VersionList(globalConfiguration.Versions));
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}
