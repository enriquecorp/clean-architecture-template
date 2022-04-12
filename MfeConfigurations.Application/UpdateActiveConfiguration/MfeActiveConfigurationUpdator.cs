using MfeConfigurations.Domain;
using shared.domain.Bus.Event;
using Versioning.Shared.Domain.Constants;
using Versioning.Shared.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.UpdateActiveConfiguration
{
    public sealed class MfeActiveConfigurationUpdator
    {
        private readonly IMfeTenantConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public MfeActiveConfigurationUpdator(IMfeTenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        public async Task Execute(MfeId name, MfeConfigurationName activeConfiguration, IEnumerable<TenantId> tenants)
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

        private void EnsureSupportedConfigurationName(MfeConfigurationName configuration)
        {
            if (!Configuration.SupportedConfigurations.Contains(configuration.Value))
            {
                throw new ConfigurationNotSupportedException(configuration);
            }
        }
    }
}
