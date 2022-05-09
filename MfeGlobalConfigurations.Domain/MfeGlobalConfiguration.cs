using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class MfeGlobalConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }

        public MfeConfigurationName ActiveConfiguration { get; private set; }

        public ConfigurationList Configurations { get; private set; }

        public MfeGlobalConfiguration(MfeId name, MfeConfigurationName active, ConfigurationList configurations)
        {
            this.MfeId = name;
            this.ActiveConfiguration = active;
            this.Configurations = configurations;
        }

        public static MfeGlobalConfiguration Create(MfeId name, ConfigurationList configurations, MfeConfigurationName active)
        {
            var activeConfiguration = active.IsEmpty() ? GetFirstConfiguration(configurations) : active;
            var configuration = new MfeGlobalConfiguration(name, activeConfiguration, configurations);
            configuration.Record(new GlobalConfigurationCreatedDomainEvent(name.Value, configurations.Value, activeConfiguration.Value));

            return configuration;
        }

        /// <summary>
        /// This will merge the incoming configuration list with the current one
        /// </summary>
        /// <param name="configurations"></param>
        public void Update(MfeConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            if (!activeConfiguration.IsEmpty() && this.ActiveConfiguration != activeConfiguration)
            {
                this.ActiveConfiguration = activeConfiguration;
                this.Record(new GlobalActiveConfigurationChangedDomainEvent(this.MfeId.Value, activeConfiguration.Value));
            }
            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersionUrl);
                if (incomingVersionUrl != null && this.Configurations[item.Key] != incomingVersionUrl)
                {
                    // it will update only if the incoming versionurl has a value and it is different than current value
                    this.Configurations[item.Key] = incomingVersionUrl;
                    this.Record(new GlobalVersionChangedDomainEvent(this.MfeId.Value, item.Key.Value, incomingVersionUrl.Value));
                }
            }
        }

        private static MfeConfigurationName GetFirstConfiguration(ConfigurationList configurations) => configurations.GetFirstConfigurationName();
    }
}
