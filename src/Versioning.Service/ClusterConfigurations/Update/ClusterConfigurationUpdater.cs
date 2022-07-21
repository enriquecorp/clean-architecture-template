using Versioning.Domain.ClusterConfigurations;
using Shared.Domain.Bus.Event;
using Versioning.Domain.ValueObjects;

namespace Versioning.Service.ClusterConfigurations.Update
{
    public sealed class ClusterConfigurationUpdater
    {
        private readonly IClusterConfigurationRepository repository;
        private readonly IEventBus eventBus;

        public ClusterConfigurationUpdater(IClusterConfigurationRepository repository, IEventBus bus)
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
        /// <param name="clusters"></param>
        /// <param name="versionUrl"></param>
        /// <param name="setConfigurationActive"></param>
        /// <returns></returns>
        public async Task Execute(MfeId name, ConfigurationName configuration, IEnumerable<ClusterId> clusters, VersionUrl versionUrl, bool setConfigurationActive)
        {
            var configurations = await this.repository.SearchBatch(name, clusters.ToList());
            var newConfigurationList = new ConfigurationList(new Dictionary<ConfigurationName, VersionUrl>() { { configuration, versionUrl } });
            foreach (var c in configurations)
            {
                c.Update(newConfigurationList);
                var changeActiveConfiguration = setConfigurationActive && c.ActiveConfiguration != configuration;
                if (changeActiveConfiguration)
                {
                    c.UpdateActiveConfiguration(configuration);
                }
            }
            await this.repository.UpdateBatch(configurations);
            configurations.ForEach(async c => await this.eventBus.Publish(c.PullDomainEvents()));
        }
    }
}
