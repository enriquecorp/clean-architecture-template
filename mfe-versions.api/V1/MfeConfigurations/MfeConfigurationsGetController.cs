using System.Net;
using mfe_versions.api.Constants;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Application.Find;
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
    public class MfeConfigurationsGetController : ApiBaseController
    {
        private readonly MfeTenantConfigurationFinder configurationFinder;

        public MfeConfigurationsGetController(MfeTenantConfigurationFinder configurationFinder)
        {
            this.configurationFinder = configurationFinder;
        }
        // GET api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(MfeConfigurationDoesntExistsException), HttpStatusCode = HttpStatusCode.NotFound)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(NoActiveConfigurationExistsException), HttpStatusCode = HttpStatusCode.NotFound)]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Get([FromHeader(Name = ApiHeaders.TENANT_ID)] string tenantId, [FromQuery]string mfeId, [FromQuery]string configuration)
        public async Task<IActionResult> Get([FromHeader(Name = ApiHeaders.TENANT_ID)] string tenantId, [FromQuery] ConfigurationVersionRequest configurationRequest)
        {
            //var response = await this.configurationFinder.Execute(new TenantId(tenantId),new MfeId(mfeId), new MfeConfigurationName(configuration));
            var response = await this.configurationFinder.Execute(new TenantId(tenantId), new MfeId(configurationRequest.MfeId), configurationRequest.Configuration != null ? new MfeConfigurationName(configurationRequest.Configuration) : null);
            return this.StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
