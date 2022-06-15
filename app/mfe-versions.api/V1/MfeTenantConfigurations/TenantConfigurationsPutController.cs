using MfeTenantConfigurations.Application.Update;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeTenantConfigurations
{
    [Tags("TenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class TenantConfigurationsPutController : ApiBaseController
    {
        private readonly TenantConfigurationUpdater configurationUpdater;

        public TenantConfigurationsPutController(TenantConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }
        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        //[TypeFilter(typeof(DomainExceptionFilter))]
        //[DomainExceptionMapper(ExceptionTypeName =nameof(ConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody] TenantConfigurationUpdateRequest request)
        {
            await this.configurationUpdater.Execute(new MfeId(request.MfeId), new ConfigurationName(request.Configuration), request.Tenants.Select(t => new TenantId(t)), new VersionUrl(request.VersionUrl), request.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
