using MfeTenantConfigurations.Application.Update;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeTenantConfigurations
{
    [Tags("MfeTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class MfeTenantConfigurationsPutController : ApiBaseController
    {
        private readonly MfeTenantConfigurationUpdator configurationUpdator;

        public MfeTenantConfigurationsPutController(MfeTenantConfigurationUpdator configurationUpdator)
        {
            this.configurationUpdator = configurationUpdator;
        }
        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        //[TypeFilter(typeof(DomainExceptionFilter))]
        //[DomainExceptionMapper(ExceptionTypeName =nameof(MfeConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Post([FromBody] MfeTenantConfigurationUpdateRequest mfeConfiguration)
        {
            await this.configurationUpdator.Execute(new MfeId(mfeConfiguration.MfeId), new MfeConfigurationName(mfeConfiguration.Configuration), mfeConfiguration.Tenants.Select(t => new TenantId(t)), new VersionUrl(mfeConfiguration.VersionUrl), mfeConfiguration.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
