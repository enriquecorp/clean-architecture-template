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
    [Tags("ActiveTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/active-tenant-configurations")]
    public class TenantActiveConfigurationsPutController : ApiBaseController
    {
        private readonly ActiveConfigurationUpdater configurationUpdater;

        public TenantActiveConfigurationsPutController(ActiveConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }

        // PUT api/v{version:apiVersion}/active-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationNotSupportedException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] ActiveConfigurationRequest configuration)
        {
            await this.configurationUpdater.Execute(new MfeId(configuration.MfeId), new ConfigurationName(configuration.ActiveConfiguration), configuration.Tenants.Select(t => new TenantId(t)));
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
