using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Web;
using Versioning.Domain.Shared.ValueObjects;
using Versioning.Service.ClusterConfigurations.Update;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeClusterConfigurations
{
    [Tags("ClusterConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-cluster-configurations")]
    public class ClusterConfigurationsPutController : ApiBaseController
    {
        private readonly ClusterConfigurationUpdater configurationUpdater;

        public ClusterConfigurationsPutController(ClusterConfigurationUpdater configurationUpdater)
        {
            this.configurationUpdater = configurationUpdater;
        }
        // PUT api/v{version:apiVersion}/mfe-tenant-configurations
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] ClusterConfigurationUpdateRequest request)
        {
            await this.configurationUpdater.Execute(new MfeId(request.MfeId),
                new ConfigurationName(request.Configuration), request.Clusters.Select(t => new ClusterId(t)),
                new VersionUrl(request.VersionUrl), request.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
