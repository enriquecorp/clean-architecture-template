using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Domain.Exceptions;
using shared.domain.Bus.Event;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationUpdator
    {
        private readonly IMfeGlobalConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public MfeGlobalConfigurationUpdator(IMfeGlobalConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        public async Task Execute(MfeId name, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            var configuration = await this.repository.Search(name);
            if (configuration == null)
            {
                configuration = MfeGlobalConfiguration.Create(name, configurations, activeConfiguration);
                await this.repository.Save(configuration);
                await this.eventBus.Publish(configuration.PullDomainEvents());
                return;
            }
            configuration.Update(activeConfiguration, configurations);
            await this.repository.Update(configuration);
            await this.eventBus.Publish(configuration.PullDomainEvents());
        }

        private void EnsureVersionsAreNotEmpty(MfeId name, ConfigurationList configurations)
        {
            if (configurations == null || configurations.Length < 0)
            {
                throw new MfeVersionsAreEmpty(name);
            }
        }
    }
}
