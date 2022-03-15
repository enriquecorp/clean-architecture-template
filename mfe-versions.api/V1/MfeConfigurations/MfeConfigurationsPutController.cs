using System.Net;
using mfe_versions.api.Constants;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Application.Update;
using MfeConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class MfeConfigurationsPutController : ApiBaseController
    {
        private readonly MfeTenantConfigurationUpdator configurationUpdator;

        public MfeConfigurationsPutController(MfeTenantConfigurationUpdator configurationUpdator)
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
            await this.configurationUpdator.Execute(new MfeId(mfeConfiguration.MfeId), new MfeConfigurationName(mfeConfiguration.Configuration), mfeConfiguration.Tenants.Select(t => new TenantId(t)), new MfeVersion(mfeConfiguration.Version), mfeConfiguration.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
