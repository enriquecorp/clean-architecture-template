using MfeConfigurations.Domain;
using MfeConfigurations.Domain.Exceptions;
using MfeGlobalConfigurations.Domain;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Find
{

    public sealed class MfeTenantConfigurationFinder
    {
        private readonly IMfeTenantConfigurationRepository repository;
        //TODO: Remove GlobalFinder once we implement QueryBus
        //TODO: and remove project reference from MfeConfigurations.Application project!
        private readonly MfeGlobalConfigurationFinder globalConfigurationFinder;

        public MfeTenantConfigurationFinder(IMfeTenantConfigurationRepository repository, MfeGlobalConfigurationFinder globalFinder)
        {
            this.repository = repository;
            this.globalConfigurationFinder = globalFinder;
        }

        public async Task<ConfigurationVersionResponse> Execute(TenantId tenantId, MfeId name, MfeConfigurationName? configurationName)
        {
            var configuration = await this.repository.Search(name, tenantId);

            if (configuration == null)
            {
                // throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
                //look up Global configuration!!!! Share Domain Service or use GlobalConfigurationQueryBus
                MfeGlobalConfiguration? globalConfiguration = await this.globalConfigurationFinder.Find(name);
                if (globalConfiguration == null)
                {
                    throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
                }
                configuration = new MfeTenantConfiguration(tenantId, name, globalConfiguration.ActiveConfiguration, globalConfiguration.Configurations);
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

        private void EnsureActiveConfigurationIsNotEmpty(TenantId tenantId, MfeId name, MfeTenantConfiguration configuration)
        {
            if (configuration.ActiveConfiguration.IsEmpty())
            {
                throw new NoActiveConfigurationExistsException(tenantId, name);
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
