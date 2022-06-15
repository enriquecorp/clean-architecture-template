using MfeClusterConfigurations.Domain.Events;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
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
    }
}
