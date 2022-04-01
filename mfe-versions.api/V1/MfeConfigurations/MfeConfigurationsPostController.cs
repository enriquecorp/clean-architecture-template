using System.Net;
using mfe_versions.api.Constants;
using MfeConfigurations.Application.Create;
using MfeConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeConfigurations
{
    [Tags("MfeTenantConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-tenant-configurations")]
    public class MfeConfigurationsPostController : ApiBaseController
    {
        private readonly MfeTenantConfigurationCreator configurationCreator; //= new();

        public MfeConfigurationsPostController(MfeTenantConfigurationCreator configurationCreator)
        {
            this.configurationCreator = configurationCreator;
        }

        // POST api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName =nameof(MfeConfigurationAlreadyExistsException), HttpStatusCode = HttpStatusCode.Conflict)]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] MfeTenantConfigurationRequest mfeConfiguration)
        {
            await this.configurationCreator.Execute(mfeConfiguration);
            return this.StatusCode(StatusCodes.Status201Created);
        }
    }
}
