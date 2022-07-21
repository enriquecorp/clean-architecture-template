using Shared.Domain.Aggregate;
using Versioning.Domain.TenantConfigurations.Events;
using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.TenantConfigurations
{
    public sealed class TenantConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }
        public TenantId TenantId { get; private set; }
        public ConfigurationName ActiveConfiguration { get; private set; }
        public ConfigurationList Configurations { get; private set; }

        public TenantConfiguration(TenantId id, MfeId name, ConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            this.TenantId = id;
            this.MfeId = name;
            this.ActiveConfiguration = activeConfiguration;
            this.Configurations = configurations;
        }

        public static TenantConfiguration Create(MfeId name, TenantId id, ConfigurationList configurations, ConfigurationName activeConfiguration)
        {
            var configuration = new TenantConfiguration(id, name, activeConfiguration, configurations);
            configuration.Record(new TenantConfigurationCreatedDomainEvent($"{name.Value}#{id.Value}", configurations.Value, activeConfiguration.Value));

            return configuration;
        }

        public void UpdateConfigurations(ConfigurationList configurations)
        {
            // this.Versions = versions;
            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersionUrl);
                if (incomingVersionUrl != null && this.Configurations.ContainsKey(item.Key))
                {
                    this.Configurations[item.Key] = incomingVersionUrl; // it will update only if the incoming versionUrl has a value
                    this.Record(new VersionUrlChangedDomainEvent($"{this.MfeId.Value}#{this.TenantId.Value}", configurationName: item.Key.Value, versionUrl: incomingVersionUrl.Value));
                }
            }
        }

        public void UpdateActiveConfiguration(ConfigurationName configuration)
        {
            this.ActiveConfiguration = configuration;
            this.Record(new ActiveTenantConfigurationChangedDomainEvent($"{this.MfeId.Value}#{this.TenantId.Value}", this.ActiveConfiguration.Value));
        }
    }
}
