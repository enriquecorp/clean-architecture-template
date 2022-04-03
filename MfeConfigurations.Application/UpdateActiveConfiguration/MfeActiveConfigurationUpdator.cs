using MfeConfigurations.Domain;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.UpdateActiveConfiguration
{
    public sealed class MfeActiveConfigurationUpdator
    {
        private readonly IMfeTenantConfigurationRepository repository;

        public MfeActiveConfigurationUpdator(IMfeTenantConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task Execute(MfeId name, MfeConfigurationName activeConfiguration, IEnumerable<TenantId> tenants)
        {
            this.EnsureSupportedConfigurationName(activeConfiguration);

            var configurations = await this.repository.SearchBatch(name, tenants.ToList());
            foreach (var c in configurations)
            {
                c.UpdateActiveConfiguration(activeConfiguration);
            }
            //if (configuration == null)
            //{
            //    configuration = MfeGlobalConfiguration.Create(name, configurations);
            //    await this.repository.Save(configuration);
            //    // $this->bus->publish(...$course->pullDomainEvents());
            //    return;
            //}
            //configuration.UpdateVersions(configurations);
            await this.repository.UpdateBatch(configurations);
            //// $this->bus->publish(...$course->pullDomainEvents());
        }

        private void EnsureSupportedConfigurationName(MfeConfigurationName configuration)
        {
            if (!Configuration.SupportedConfigurations.Contains(configuration.Value))
            {
                throw new ConfigurationNotSupportedException(configuration);
            }
        }
    }
}
