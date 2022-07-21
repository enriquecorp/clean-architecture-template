using Shared.Domain.Aggregate;
using Versioning.Domain.GlobalConfigurations.Events;
using Versioning.Domain.ValueObjects;

namespace Versioning.Domain.GlobalConfigurations
{
    public sealed class GlobalConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }

        public ConfigurationName ActiveConfiguration { get; private set; }

        public ConfigurationList Configurations { get; private set; }

        public GlobalConfiguration(MfeId name, ConfigurationName active, ConfigurationList configurations)
        {
            this.MfeId = name;
            this.ActiveConfiguration = active;
            this.Configurations = configurations;
        }

        public static GlobalConfiguration Create(MfeId name, ConfigurationList configurations, ConfigurationName active)
        {
            var activeConfiguration = active.IsEmpty() ? GetFirstConfiguration(configurations) : active;
            var configuration = new GlobalConfiguration(name, activeConfiguration, configurations);
            configuration.Record(new GlobalConfigurationCreatedDomainEvent(name.Value, configurations.Value, activeConfiguration.Value));

            return configuration;
        }

        /// <summary>
        /// This will merge the incoming configuration list with the current one
        /// </summary>
        /// <param name="configurations"></param>
        public void Update(ConfigurationName activeConfiguration, ConfigurationList configurations)
        {
            if (!activeConfiguration.IsEmpty() && this.ActiveConfiguration != activeConfiguration)
            {
                this.ActiveConfiguration = activeConfiguration;
                this.Record(new GlobalActiveConfigurationChangedDomainEvent(this.MfeId.Value, activeConfiguration.Value));
            }
            foreach (var item in this.Configurations)
            {
                configurations.TryGetValue(item.Key, out var incomingVersionUrl);
                if (incomingVersionUrl != null && !string.IsNullOrEmpty(incomingVersionUrl.Value) && this.Configurations[item.Key] != incomingVersionUrl)
                {
                    // it will update only if the incoming versionurl has a value and it is different than current value
                    this.Configurations[item.Key] = incomingVersionUrl;
                    this.Record(new GlobalVersionChangedDomainEvent(this.MfeId.Value, item.Key.Value, incomingVersionUrl.Value));
                }
            }
        }

        private static ConfigurationName GetFirstConfiguration(ConfigurationList configurations) => configurations.GetFirstConfigurationName();
    }
}
