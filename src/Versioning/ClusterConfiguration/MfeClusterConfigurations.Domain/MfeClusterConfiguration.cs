using MfeClusterConfigurations.Domain.Events;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeClusterConfigurations.Domain
{
    public sealed class MfeClusterConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }
        public ClusterId ClusterId { get; private set; }
        public MfeConfigurationName ActiveConfiguration { get; private set; }
        public ConfigurationList Configurations { get; private set; }

        public MfeClusterConfiguration(ClusterId id, MfeId name, MfeConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            this.ClusterId = id;
            this.MfeId = name;
            this.ActiveConfiguration = activeConfiguration;
            this.Configurations = configurations;
        }

        public static MfeClusterConfiguration Create(MfeId name, ClusterId id, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            var configuration = new MfeClusterConfiguration(id, name, activeConfiguration, configurations);
            configuration.Record(new MfeClusterConfigurationCreatedDomainEvent($"{name.Value}#{id.Value}", configurations.Value, activeConfiguration.Value));

            return configuration;
        }
    }
}
