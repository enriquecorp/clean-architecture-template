using Versioning.Domain.GlobalConfigurations;
using Versioning.Domain.GlobalConfigurations.Exceptions;
using Shared.Domain.Bus.Event;
using Versioning.Domain.ValueObjects;

namespace Versioning.Service.GlobalConfigurations.Update
{
    public sealed class GlobalConfigurationUpdater
    {
        private readonly IGlobalConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public GlobalConfigurationUpdater(IGlobalConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        public async Task Execute(MfeId name, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            this.EnsureConfigurationsAreNotEmpty(name, configurations);

            var configuration = await this.repository.Search(name);
            if (configuration == null)
            {
                configuration = GlobalConfiguration.Create(name, configurations, activeConfiguration);
                await this.repository.Save(configuration);
                await this.eventBus.Publish(configuration.PullDomainEvents());
                return;
            }
            configuration.Update(activeConfiguration, configurations);
            await this.repository.Update(configuration);
            await this.eventBus.Publish(configuration.PullDomainEvents());
        }

        private void EnsureConfigurationsAreNotEmpty(MfeId name, ConfigurationList configurations)
        {
            if (configurations == null || configurations.Length < 0)
            {
                throw new ConfigurationsAreEmpty(name);
            }
        }
    }
}
