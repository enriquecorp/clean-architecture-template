using Versioning.Domain.ClusterConfigurations.Events;
using Shared.Domain.Aggregate;
using Versioning.Domain.Shared.ValueObjects;

namespace Versioning.Domain.ClusterConfigurations
{
    public sealed class ClusterConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }
        public ClusterId ClusterId { get; private set; }
        public ConfigurationName ActiveConfiguration { get; private set; }
        public ConfigurationList Configurations { get; private set; }

        public ClusterConfiguration(ClusterId id, MfeId name, ConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            this.ClusterId = id;
            this.MfeId = name;
            this.ActiveConfiguration = activeConfiguration;
            this.Configurations = configurations;
        }

        public static ClusterConfiguration Create(MfeId name, ClusterId id, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            var configuration = new ClusterConfiguration(id, name, activeConfiguration, configurations);
            configuration.Record(new ClusterConfigurationCreatedDomainEvent($"{name.Value}#{id.Value}", configurations.Value, activeConfiguration.Value));

            return configuration;
        }

        public void Update(ConfigurationList configurations)
        {

            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersionUrl);
                if (incomingVersionUrl != null && !string.IsNullOrEmpty(incomingVersionUrl.Value) && this.Configurations[item.Key] != incomingVersionUrl)
                {
                    // it will update only if the incoming versionurl has a value and it is different than current value
                    this.Configurations[item.Key] = incomingVersionUrl;
                    //this.Record(new GlobalVersionChangedDomainEvent(this.MfeId.Value, item.Key.Value, incomingVersionUrl.Value));
                }
            }
        }

        public void UpdateActiveConfiguration(ConfigurationName configuration)
        {
            if (!configuration.IsEmpty() && this.ActiveConfiguration != configuration)
            {
                this.ActiveConfiguration = configuration;
                //this.Record(new GlobalActiveConfigurationChangedDomainEvent(this.MfeId.Value, activeConfiguration.Value));
            }
        }

        public void Update(ConfigurationName configuration, VersionUrl incomingVersionUrl)
        {

            this.Configurations.TryGetValue(configuration, out var existingUrl);
            if (incomingVersionUrl != null && !string.IsNullOrEmpty(incomingVersionUrl.Value) && this.Configurations[configuration] != incomingVersionUrl)
            {
                // it will update only if the incoming versionurl has a value and it is different than current value
                this.Configurations[configuration] = incomingVersionUrl;
                //this.Record(new GlobalVersionChangedDomainEvent(this.MfeId.Value, item.Key.Value, incomingVersionUrl.Value));
            }
        }
    }
}
