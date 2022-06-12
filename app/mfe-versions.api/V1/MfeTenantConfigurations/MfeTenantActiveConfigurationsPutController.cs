using System.Net;
using MfeTenantConfigurations.Application.UpdateActiveConfiguration;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeTenantConfigurations
{
    [Tags("MfeActiveTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-active-tenant-configurations")]
    public class MfeTenantActiveConfigurationsPutController : ApiBaseController
    {
        private readonly MfeActiveConfigurationUpdater configurationUpdater;

        public MfeTenantActiveConfigurationsPutController(MfeActiveConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }

        // PUT api/v{version:apiVersion}/mfe-active-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationNotSupportedException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] MfeActiveConfigurationRequest configuration)
        {
            await this.configurationUpdater.Execute(new MfeId(configuration.MfeId), new MfeConfigurationName(configuration.ActiveConfiguration), configuration.Tenants.Select(t => new TenantId(t)));
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
