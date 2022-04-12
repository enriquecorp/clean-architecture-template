using MfeConfigurations.Domain;
using shared.domain.Bus.Event;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Application.Update
{
    public sealed class MfeTenantConfigurationUpdator
    {
        private readonly IMfeTenantConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public MfeTenantConfigurationUpdator(IMfeTenantConfigurationRepository repository, IEventBus bus)
        {
            this.repository = repository;
            this.eventBus = bus;
        }

        /// <summary>
        /// It will update the {version} for a given {configuration} for one microfrontend {name} & {tenants} list
        /// Optionally, it will set that {configuration} as active
        /// </summary>
        /// <param name="name"></param>
        /// <param name="configuration"></param>
        /// <param name="tenants"></param>
        /// <param name="version"></param>
        /// <param name="setConfigurationActive"></param>
        /// <returns></returns>
        public async Task Execute(MfeId name, MfeConfigurationName configuration, IEnumerable<TenantId> tenants, MfeVersion version, bool setConfigurationActive)
        {
            //this.EnsureVersionsAreNotEmpty(name, versions);
            var configurations = await this.repository.SearchBatch(name, tenants.ToList());
            foreach (var c in configurations)
            {
                var newConfigurationList = new ConfigurationList(new Dictionary<MfeConfigurationName, MfeVersion>() { { configuration, version } });
                c.UpdateConfigurations(newConfigurationList);
                if (setConfigurationActive && c.ActiveConfiguration != configuration)
                {
                    c.UpdateActiveConfiguration(configuration);
                }
            }
            await this.repository.UpdateBatch(configurations);
            configurations.ForEach(async c => await this.eventBus.Publish(c.PullDomainEvents()));
            // $this->bus->publish(...$course->pullDomainEvents());
        }
    }
}
