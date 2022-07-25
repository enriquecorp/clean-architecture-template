using Versioning.Domain.ClusterConfigurations;
using Versioning.Domain.ClusterConfigurations.Exceptions;
using Versioning.Domain.GlobalConfigurations;
using Versioning.Domain.Shared.Constants;
using Versioning.Domain.Shared.Exceptions;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Service.ClusterConfigurations.Find
{

    public sealed class ClusterConfigurationFinder
    {
        private readonly IClusterConfigurationRepository repository;
        //TODO: Remove GlobalFinder once we implement QueryBus
        //TODO: and remove project reference from MfeClusterConfigurations.Application project!
        private readonly GlobalConfigurationFinder globalConfigurationFinder;

        public ClusterConfigurationFinder(IClusterConfigurationRepository repository, GlobalConfigurationFinder globalFinder)
        {
            this.repository = repository;
            this.globalConfigurationFinder = globalFinder;
        }

        public async Task<ClusterConfigurationVersionResponse> Execute(ClusterId clusterId, MfeId name, ConfigurationName? configurationName)
        {
            var source = "cluster";
            var configuration = await this.repository.Search(name, clusterId);

            if (configuration == null)
            {
                //    // throw new ConfigurationDoesntExistsException(tenantId, name, configurationName);
                //    //look up Global configuration!!!! Share Domain Service or use GlobalConfigurationQueryBus
                //Give me the global configuration
                GlobalConfiguration? globalConfiguration = await this.globalConfigurationFinder.Find(name);
                if (globalConfiguration == null)
                {
                    throw new ClusterConfigurationDoesntExistsException(clusterId, name, configurationName);
                }
                configuration = new ClusterConfiguration(clusterId, name, globalConfiguration.ActiveConfiguration, globalConfiguration.Configurations);
                source = "global";
            }

            if (configurationName is null)
            {
                this.EnsureActiveConfigurationIsValid(clusterId, name, configuration);
            }
            else
            {
                this.EnsureConfigurationIsValid(clusterId, name, configurationName, configuration);

            }
            var versionUrl = configurationName is not null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            return new ClusterConfigurationVersionResponse() { VersionUrl = versionUrl.Value, ConfigurationSource = $"{source} - {(configurationName is not null ? configurationName.Value : "active")}" };
        }

        private void EnsureConfigurationIsValid(ClusterId clusterId, MfeId name, ConfigurationName configurationName, ClusterConfiguration configuration)
        {
            this.EnsureSupportedConfigurationName(configurationName);
            if (string.IsNullOrEmpty(configuration.Configurations[configurationName].Value))
            {
                throw new ClusterInvalidConfigurationException(clusterId, name, configurationName);
            }
        }

        private void EnsureActiveConfigurationIsValid(ClusterId clusterId, MfeId name, ClusterConfiguration configuration)
        {
            this.EnsureActiveConfigurationIsNotEmpty(clusterId, name, configuration);
            if (!configuration.Configurations.ContainsKey(configuration.ActiveConfiguration) || string.IsNullOrEmpty(configuration.Configurations[configuration.ActiveConfiguration].Value))
            {
                throw new ClusterInvalidActiveConfigurationException(clusterId, name, configuration.ActiveConfiguration);
            }
        }

        private void EnsureActiveConfigurationIsNotEmpty(ClusterId clusterId, MfeId name, ClusterConfiguration configuration)
        {
            if (configuration.ActiveConfiguration.IsEmpty())
            {
                throw new NoActiveClusterConfigurationExistsException(clusterId, name);
            }
        }

        private void EnsureSupportedConfigurationName(ConfigurationName? name)
        {
            if (name != null && !Configuration.SupportedConfigurations.Contains(name.Value))
            {
                throw new ConfigurationNotSupportedException(name);

            }
        }
    }
}
