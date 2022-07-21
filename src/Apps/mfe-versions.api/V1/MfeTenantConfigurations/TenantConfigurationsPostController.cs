using System.Net;
using Versioning.Service.TenantConfigurations.Create;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Web;
using Shared.Infrastructure.Web.Attributes;
using Shared.Infrastructure.Web.Filters;
using Versioning.Domain.TenantConfigurations.Exceptions;


namespace mfe_versions.api.V1.MfeTenantConfigurations
{
    [Tags("TenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class TenantConfigurationsPostController : ApiBaseController
    {
        private readonly TenantConfigurationCreator configurationCreator; //= new();

        public TenantConfigurationsPostController(TenantConfigurationCreator configurationCreator)
        {
            this.configurationCreator = configurationCreator;
        }

        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(TenantConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TenantConfigurationRequest request)
        {
            await this.configurationCreator.Execute(request);
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}
