using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeConfigurations.Domain
{
    public sealed class MfeTenantConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }
        public TenantId TenantId { get; private set; }
        public MfeConfigurationName ActiveConfiguration { get; private set; }
        public ConfigurationList Configurations { get; private set; }

        public MfeTenantConfiguration(TenantId id, MfeId name, MfeConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            this.TenantId = id;
            this.MfeId = name;
            this.ActiveConfiguration = activeConfiguration;
            this.Configurations = configurations;
        }

        public static MfeTenantConfiguration Create(MfeId name, TenantId id, ConfigurationList configurations, MfeConfigurationName activeConfiguration)
        {
            var configuration = new MfeTenantConfiguration(id, name, activeConfiguration, configurations);
            configuration.Record(new MfeTenantConfigurationCreatedDomainEvent($"{name.Value}#{id.Value}", configurations.Value, activeConfiguration.Value));

            //configuration.Record(
            //new MfeConfigurationDomainEvent(
            //    id.Value, name.Value));

            return configuration;
        }

        public void UpdateConfigurations(ConfigurationList configurations)
        {
            // this.Versions = versions;
            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersion);
                if (incomingVersion != null)
                {
                    this.Configurations[item.Key] = incomingVersion; // it will update only if the incoming version has a value
                }
            }
        }

        public void UpdateActiveConfiguration(MfeConfigurationName configuration)
        {
            this.ActiveConfiguration = configuration;
        }
    }
}
