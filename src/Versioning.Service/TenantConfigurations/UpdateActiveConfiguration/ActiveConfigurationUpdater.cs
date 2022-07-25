using Shared.Domain.Bus.Event;
using Versioning.Domain.Shared.Constants;
using Versioning.Domain.Shared.Exceptions;
using Versioning.Domain.Shared.ValueObjects;
using Versioning.Domain.TenantConfigurations;

namespace Versioning.Service.TenantConfigurations.UpdateActiveConfiguration
{
    public sealed class ActiveConfigurationUpdater
    {
        private readonly ITenantConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public ActiveConfigurationUpdater(ITenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        public async Task Execute(MfeId name, ConfigurationName activeConfiguration, IEnumerable<TenantId> tenants)
        {
            this.EnsureSupportedConfigurationName(activeConfiguration);

            var configurations = await this.repository.SearchBatch(name, tenants.ToList());
            foreach (var c in configurations)
            {
                c.UpdateActiveConfiguration(activeConfiguration);
            }
            await this.repository.UpdateBatch(configurations);
            configurations.ForEach(async c => await this.eventBus.Publish(c.PullDomainEvents()));
            // $this->bus->publish(...$course->pullDomainEvents());

        }

        private void EnsureSupportedConfigurationName(ConfigurationName configuration)
        {
            if (!Configuration.SupportedConfigurations.Contains(configuration.Value))
            {
                throw new ConfigurationNotSupportedException(configuration);
            }
        }
    }
}
