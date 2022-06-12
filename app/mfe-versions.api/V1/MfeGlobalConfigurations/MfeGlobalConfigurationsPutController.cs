using System.Net;
using MfeGlobalConfigurations.Application.Update;
using MfeGlobalConfigurations.Domain.Exceptions;
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
        private readonly MfeGlobalConfigurationUpdater configurationUpdater; //= new();

        public MfeGlobalConfigurationsPutController(MfeGlobalConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }

        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationsAreEmpty), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] MfeGlobalConfigurationRequest globalConfiguration)
        {
            await this.configurationUpdater.Execute(new MfeId(globalConfiguration.MfeId), new ConfigurationList(globalConfiguration.Configurations), new MfeConfigurationName(globalConfiguration.ActiveConfiguration));
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
