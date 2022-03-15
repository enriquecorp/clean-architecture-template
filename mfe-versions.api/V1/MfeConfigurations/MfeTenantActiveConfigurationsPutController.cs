using System.Net;
using mfe_versions.api.Constants;
using MfeConfigurations.Application.UpdateActiveConfiguration;
using MfeConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeActiveTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-active-tenant-configurations")]
    public class MfeTenantActiveConfigurationsPutController : ApiBaseController
    {
        private readonly MfeActiveConfigurationUpdator configurationUpdator;

        public MfeTenantActiveConfigurationsPutController(MfeActiveConfigurationUpdator configurationUpdator)
        {
            this.configurationUpdator = configurationUpdator;
        }

        // PUT api/v{version:apiVersion}/mfe-active-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationNotSupportedException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] MfeActiveConfigurationRequest configuration)
        {
            await this.configurationUpdator.Execute(new MfeId(configuration.MfeId), new MfeConfigurationName(configuration.ActiveConfiguration), configuration.Tenants.Select(t => new TenantId(t)));
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
