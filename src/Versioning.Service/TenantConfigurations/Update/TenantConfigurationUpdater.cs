using Shared.Domain.Bus.Event;
using Versioning.Domain.Shared.ValueObjects;
using Versioning.Domain.TenantConfigurations;

namespace Versioning.Service.TenantConfigurations.Update
{
    public sealed class TenantConfigurationUpdater
    {
        private readonly ITenantConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public TenantConfigurationUpdater(ITenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        /// <summary>
        /// It will update the {versionUrl} for a given {configuration} for one microfrontend {name} & {tenants} list
        /// Optionally, it will set that {configuration} as active
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <param name="tenants"></param>
        /// <param name="versionUrl"></param>
        /// <param name="setConfigurationActive"></param>
        /// <returns></returns>
        public async Task Execute(MfeId name, ConfigurationName configuration, IEnumerable<TenantId> tenants, VersionUrl versionUrl, bool setConfigurationActive)
        {
            //this.EnsureVersionsAreNotEmpty(name, versions);
            var configurations = await this.repository.SearchBatch(name, tenants.ToList());
            foreach (var c in configurations)
            {
                var newConfigurationList = new ConfigurationList(new Dictionary<ConfigurationName, VersionUrl>() { { configuration, versionUrl } });
                c.UpdateConfigurations(newConfigurationList);
                if (setConfigurationActive && c.ActiveConfiguration != configuration)
                {
                    c.UpdateActiveConfiguration(configuration);
                }
            }
            await this.repository.UpdateBatch(configurations);
            configurations.ForEach(async c => await this.eventBus.Publish(c.PullDomainEvents()));
        }
    }
}
