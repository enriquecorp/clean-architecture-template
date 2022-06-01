using Microsoft.AspNetCore.Mvc;
using shared.web.infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfe_versions.api.V1.MfeClusterConfigurations
{
    [Tags("MfeClusterConfigurations")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/mfe-cluster-configurations")]
    public class MfeClusterConfigurationsDeleteController : ApiBaseController
    {

        public MfeClusterConfigurationsDeleteController()
        {

        }
        // DELETE api/v{version:apiVersion}/mfe-tenant-configurations
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete()//[FromBody] MfeTenantConfigurationUpdateRequest mfeConfiguration
        {
            //await this.configurationUpdator.Execute(new MfeId(mfeConfiguration.MfeId), new MfeConfigurationName(mfeConfiguration.Configuration), mfeConfiguration.Tenants.Select(t => new TenantId(t)), new VersionUrl(mfeConfiguration.VersionUrl), mfeConfiguration.SetConfigurationAsActive);
            return this.StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
