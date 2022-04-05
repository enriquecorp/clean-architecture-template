using MfeConfigurations.Domain;
using MfeConfigurations.Domain.Exceptions;
using shared.domain.Bus.Event;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Create
{
    public sealed class MfeTenantConfigurationCreator
    {
        private readonly IMfeTenantConfigurationRepository repository;
        private readonly MfeTenantConfigurationExistsChecker mfeConfigurationExistsChecker;
        private readonly IEventBus eventBus;

        public MfeTenantConfigurationCreator(IMfeTenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.mfeConfigurationExistsChecker = new MfeTenantConfigurationExistsChecker(repository);
            this.eventBus = bus;

        }
        public async Task Execute(MfeTenantConfigurationRequest configuration)
        {
            var mfeConfiguration = MfeTenantConfiguration.Create(new MfeId(configuration.MfeId), new TenantId(configuration.TenantId), new ConfigurationList(configuration.Configurations), MfeConfigurationName.CreateEmpty());
            if (await this.mfeConfigurationExistsChecker.Exists(mfeConfiguration))
            {
                throw new MfeConfigurationAlreadyExistsException(mfeConfiguration.TenantId, mfeConfiguration.MfeId);
            }
            await this.repository.Save(mfeConfiguration);
            await this.eventBus.Publish(mfeConfiguration.PullDomainEvents());
        }
    }
}
