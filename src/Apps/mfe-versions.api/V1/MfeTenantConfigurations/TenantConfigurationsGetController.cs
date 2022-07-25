using System.Net;
using mfe_versions.api.Constants;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Web;
using Shared.Infrastructure.Web.Attributes;
using Shared.Infrastructure.Web.Filters;
using Versioning.Domain.Shared.Exceptions;
using Versioning.Domain.Shared.ValueObjects;
using Versioning.Domain.TenantConfigurations.Exceptions;
using Versioning.Service.TenantConfigurations.Find;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeTenantConfigurations
{
    [Tags("TenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class TenantConfigurationsGetController : ApiBaseController
    {
        private readonly TenantConfigurationFinder configurationFinder;

        public TenantConfigurationsGetController(TenantConfigurationFinder configurationFinder)
        {
            this.configurationFinder = configurationFinder;
        }
        // GET api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(TenantConfigurationDoesntExistsException), HttpStatusCode = HttpStatusCode.NotFound)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(NoActiveConfigurationExistsException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationNotSupportedException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromHeader(Name = ApiHeaders.TENANT_ID)] string tenantId, [FromQuery] ConfigurationVersionRequest configurationRequest)
        {
            var response = await this.configurationFinder.Execute(new TenantId(tenantId), new MfeId(configurationRequest.MfeId), configurationRequest.Configuration != null ? new ConfigurationName(configurationRequest.Configuration) : null);
            return this.StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
