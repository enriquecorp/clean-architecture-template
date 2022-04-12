using MfeGlobalConfigurations.Domain;
using MfeGlobalConfigurations.Domain.Exceptions;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Application.Update
{
    public sealed class MfeGlobalConfigurationUpdator
    {
        private readonly IMfeGlobalConfigurationRepository repository;

        public MfeGlobalConfigurationUpdator(IMfeGlobalConfigurationRepository repository)
        {
            this.repository = repository;

        }

        public async Task Execute(MfeId name, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            var configuration = await this.repository.Search(name);
            if (configuration == null)
            {
                configuration = MfeGlobalConfiguration.Create(name, configurations, activeConfiguration);
                await this.repository.Save(configuration);
                // $this->bus->publish(...$course->pullDomainEvents());
                return;
            }
            configuration.Update(activeConfiguration, configurations);
            await this.repository.Update(configuration);
            // $this->bus->publish(...$course->pullDomainEvents());
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
