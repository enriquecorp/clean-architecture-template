using MfeClusterConfigurations.Domain;
using MfeClusterConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Application.Find
{

    public sealed class MfeClusterConfigurationFinder
    {
        private readonly IMfeClusterConfigurationRepository repository;
        ////TODO: Remove GlobalFinder once we implement QueryBus
        ////TODO: and remove project reference from MfeClusterConfigurations.Application project!
        //private readonly MfeGlobalClusterConfigurationFinder globalConfigurationFinder;

        public MfeClusterConfigurationFinder(IMfeClusterConfigurationRepository repository) //, MfeGlobalClusterConfigurationFinder globalFinder)
        {
            this.repository = repository;
            //this.globalConfigurationFinder = globalFinder;
        }

        public async Task<ClusterConfigurationVersionResponse> Execute(ClusterId clusterId, MfeId name, MfeConfigurationName? configurationName)
        {
            var configuration = await this.repository.Search(name, clusterId);

            if (configuration == null)
            {
                //    // throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
                //    //look up Global configuration!!!! Share Domain Service or use GlobalConfigurationQueryBus
                //    MfeGlobalConfiguration? globalConfiguration = await this.globalConfigurationFinder.Find(name);
                //    if (globalConfiguration == null)
                //    {
                throw new MfeClusterConfigurationDoesntExistsException(clusterId, name, configurationName);
                //    }
                //    configuration = new MfeClusterConfiguration(tenantId, name, globalConfiguration.ActiveConfiguration, globalConfiguration.Configurations);
            }

            if (configurationName is null)
            {
                this.EnsureActiveConfigurationIsNotEmpty(clusterId, name, configuration);
            }
            else
            {
                this.EnsureSupportedConfigurationName(configurationName);
            }

            var versionUrl = configurationName != null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            return new ClusterConfigurationVersionResponse() { VersionUrl = versionUrl.Value, ConfigurationName = configurationName != null ? configurationName.Value : "active" };
        }

        private void EnsureActiveConfigurationIsNotEmpty(ClusterId clusterId, MfeId name, MfeClusterConfiguration configuration)
        {
            if (configuration.ActiveConfiguration.IsEmpty())
            {
                throw new NoActiveClusterConfigurationExistsException(clusterId, name);
            }
        }

        private void EnsureSupportedConfigurationName(MfeConfigurationName? name)
        {
            if (name != null && !Configuration.SupportedConfigurations.Contains(name.Value))
            {
                throw new ConfigurationNotSupportedException(name);

            }
        }
    }
}
