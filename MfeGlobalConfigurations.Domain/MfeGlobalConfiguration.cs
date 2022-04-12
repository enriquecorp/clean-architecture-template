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

        public static MfeGlobalConfiguration Create(MfeId name, ConfigurationList versions, MfeConfigurationName? active = null)
        {
            var configuration = new MfeGlobalConfiguration(name, active ?? GetFirstConfiguration(versions), versions);

            //configuration.Record(
            //new MfeConfigurationDomainEvent(
            //    id.Value, name.Value));

            return configuration;
        }

        /// <summary>
        /// This will merge the incoming version list with the current one
        /// </summary>
        /// <param name="configurations"></param>
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

        private static MfeConfigurationName GetFirstConfiguration(ConfigurationList versions) => versions.GetFirstConfigurationName();
    }
}
