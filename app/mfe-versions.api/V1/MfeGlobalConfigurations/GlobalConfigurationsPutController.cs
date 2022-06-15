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
    [Tags("GlobalConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-global-configurations")]
    public class GlobalConfigurationsPutController : ApiBaseController
    {
        private readonly GlobalConfigurationUpdater configurationUpdater; //= new();

        public GlobalConfigurationsPutController(GlobalConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }

        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationsAreEmpty), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] GlobalConfigurationRequest globalConfiguration)
        {
            await this.configurationUpdater.Execute(new MfeId(globalConfiguration.MfeId), new ConfigurationList(globalConfiguration.Configurations), new ConfigurationName(globalConfiguration.ActiveConfiguration));
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
