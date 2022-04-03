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
            //var configuration = configurationName==null? await this.repository.SearchActiveConfiguration(name, tenantId): await this.repository.SearchByConfigurationName(name, tenantId, configurationName);
            this.EnsureSupportedConfigurationName(configurationName);

            if (configuration == null)
            {
                throw new MfeConfigurationDoesntExistsException(tenantId, name, configurationName);
            }
            this.EnsureActiveConfigurationIsNotEmpty(tenantId, name, configuration);

            var version = configurationName != null ? configuration.Configurations[configurationName] : configuration.Configurations[configuration.ActiveConfiguration];
            //await this.repository.Save(configuration);
            // $this->bus->publish(...$course->pullDomainEvents());
            return new ConfigurationVersionResponse() { Version = version.Value, ConfigurationName = configurationName != null ? configurationName.Value : "active" };

            //configuration.UpdateVersions(configurations);

            //// $this->bus->publish(...$course->pullDomainEvents());
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
