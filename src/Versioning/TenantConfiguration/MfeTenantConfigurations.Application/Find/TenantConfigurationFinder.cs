using MfeGlobalConfigurations.Domain;
using MfeTenantConfigurations.Domain;
using MfeTenantConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeTenantConfigurations.Application.Find
{

    public sealed class TenantConfigurationFinder
    {
        private readonly ITenantConfigurationRepository repository;
        //TODO: Remove GlobalFinder once we implement QueryBus
        //TODO: and remove project reference from Configurations.Application project!
        private readonly GlobalConfigurationFinder globalConfigurationFinder;

        public TenantConfigurationFinder(ITenantConfigurationRepository repository, GlobalConfigurationFinder globalFinder)
        {
            this.repository = repository;
            this.globalConfigurationFinder = globalFinder;
        }

        public async Task<ConfigurationVersionResponse> Execute(TenantId tenantId, MfeId name, ConfigurationName? configurationName)
        {
            var configuration = await this.repository.Search(name, tenantId);

            if (configuration == null)
            {
                // throw new ConfigurationDoesntExistsException(tenantId, name, configurationName);
                //look up Global configuration!!!! Share Domain Service or use GlobalConfigurationQueryBus
                var globalConfiguration = await this.globalConfigurationFinder.Find(name);
                if (globalConfiguration == null)
                {
                    throw new TenantConfigurationDoesntExistsException(tenantId, name, configurationName);
                }
                configuration = new TenantConfiguration(tenantId, name, globalConfiguration.ActiveConfiguration, globalConfiguration.Configurations);
            }

            if (configurationName is null)
            {
                this.EnsureActiveConfigurationIsNotEmpty(tenantId, name, configuration);
            }
            else
            {
                this.EnsureSupportedConfigurationName(configurationName);
            }

            var versionUrl = configurationName != null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            return new ConfigurationVersionResponse() { VersionUrl = versionUrl.Value, ConfigurationName = configurationName != null ? configurationName.Value : "active" };
        }

        private void EnsureActiveConfigurationIsNotEmpty(TenantId tenantId, MfeId name, TenantConfiguration configuration)
        {
            if (configuration.ActiveConfiguration.IsEmpty())
            {
                throw new NoActiveConfigurationExistsException(tenantId, name);
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
