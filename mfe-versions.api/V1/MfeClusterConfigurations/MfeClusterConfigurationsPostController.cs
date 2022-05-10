using System.Net;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;


namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class MfeClusterConfigurationsPostController : ApiBaseController
    {
        private readonly MfeTenantConfigurationCreator configurationCreator; //= new();

        public MfeClusterConfigurationsPostController(MfeTenantConfigurationCreator configurationCreator)
        {
            this.configurationCreator = configurationCreator;
        }

        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(MfeConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] MfeTenantConfigurationRequest mfeConfiguration)
        {
            await this.configurationCreator.Execute(mfeConfiguration);
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}
