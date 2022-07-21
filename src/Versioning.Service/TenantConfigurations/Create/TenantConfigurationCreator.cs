using Versioning.Domain.TenantConfigurations;
using Versioning.Domain.TenantConfigurations.Exceptions;
using Shared.Domain.Bus.Event;
using Versioning.Domain.ValueObjects;

namespace Versioning.Service.TenantConfigurations.Create
{
    public sealed class TenantConfigurationCreator
    {
        private readonly ITenantConfigurationRepository repository;
        private readonly TenantConfigurationExistsChecker configurationExistsChecker;
        private readonly IEventBus eventBus;

        public TenantConfigurationCreator(ITenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.configurationExistsChecker = new TenantConfigurationExistsChecker(repository);
            this.eventBus = bus;

        }
        public async Task Execute(TenantConfigurationRequest request)
        {
            var configuration = TenantConfiguration.Create(new MfeId(request.MfeId), new TenantId(request.TenantId), new ConfigurationList(request.Configurations), string.IsNullOrEmpty(request.ActiveConfiguration) ? ConfigurationName.CreateEmpty() : new ConfigurationName(request.ActiveConfiguration));
            if (await this.configurationExistsChecker.Exists(configuration))
            {
                throw new TenantConfigurationAlreadyExistsException(configuration.TenantId, configuration.MfeId);
            }
            await this.repository.Save(configuration);
            await this.eventBus.Publish(configuration.PullDomainEvents());
        }
    }
}
