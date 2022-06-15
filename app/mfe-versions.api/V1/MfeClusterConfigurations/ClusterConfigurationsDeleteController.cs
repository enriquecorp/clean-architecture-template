using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeClusterConfigurations
{
    [Tags("ClusterConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-cluster-configurations")]
    public class ClusterConfigurationsDeleteController : ApiBaseController
    {

        public ClusterConfigurationsDeleteController()
        {

        }
        // DELETE api/v{version:apiVersion}/mfe-tenant-configurations
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete()//[FromBody] MfeTenantConfigurationUpdateRequest configuration
        {
            //await this.configurationUpdater.Execute(new MfeId(configuration.MfeId), new ConfigurationName(configuration.Configuration), configuration.Tenants.Select(t => new TenantId(t)), new VersionUrl(configuration.VersionUrl), configuration.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
