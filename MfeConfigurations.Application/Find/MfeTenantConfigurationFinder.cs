using MfeConfigurations.Domain;
using MfeConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Find
{

    public sealed class MfeTenantConfigurationFinder
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeTenantConfigurationFinder(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task<ConfigurationVersionResponse> Execute(TenantId tenantId, MfeId name, MfeConfigurationName? configurationName)
        {
            var configuration = await this.repository.Search(name, tenantId);

            if (configuration == null)
            {
                throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
                //look up Global configuration!!!! Share Domain Service or use GlobalConfigurationQueryBus
            }

            if (configurationName is null)
            {
                this.EnsureActiveConfigurationIsNotEmpty(tenantId, name, configuration);
            }
            else
            {
                this.EnsureSupportedConfigurationName(configurationName);
            }

            var version = configurationName != null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            return new ConfigurationVersionResponse() { Version = version.Value, ConfigurationName = configurationName != null ? configurationName.Value : "active" };
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
