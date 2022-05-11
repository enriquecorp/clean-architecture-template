using System.Net;
using mfe_versions.api.Constants;
using MfeClusterConfigurations.Application.Find;
using MfeClusterConfigurations.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;
using shared.web.infrastructure.Attributes;
using shared.web.infrastructure.Filters;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeClusterConfigurations
{
    [Tags("MfeClusterConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-cluster-configurations")]
    public class MfeClusterConfigurationsGetController : ApiBaseController
    {
        private readonly MfeClusterConfigurationFinder configurationFinder;

        public MfeClusterConfigurationsGetController(MfeClusterConfigurationFinder configurationFinder)
        {
            this.configurationFinder = configurationFinder;
        }
        // GET api/v{version:apiVersion}/mfe-tenant-configurations
        [TypeFilter(typeof(DomainExceptionFilter))]
        [DomainExceptionMapper(ExceptionTypeName = nameof(MfeClusterConfigurationDoesntExistsException), HttpStatusCode = HttpStatusCode.NotFound)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(NoActiveClusterConfigurationExistsException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(ConfigurationNotSupportedException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [DomainExceptionMapper(ExceptionTypeName = nameof(MfeClusterInvalidConfigurationException), HttpStatusCode = HttpStatusCode.BadRequest)]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromHeader(Name = ApiHeaders.CLUSTER_ID)] string clusterId, [FromQuery] ClusterConfigurationVersionRequest configurationRequest)
        {
            var response = await this.configurationFinder.Execute(new ClusterId(clusterId), new MfeId(configurationRequest.MfeId), configurationRequest.Configuration != null ? new MfeConfigurationName(configurationRequest.Configuration) : null);
            return this.StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
