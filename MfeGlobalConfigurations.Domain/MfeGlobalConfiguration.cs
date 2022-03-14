using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Aggregate;
using Versioning.Shared.Domain.ValueObjects;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class MfeGlobalConfiguration : AggregateRoot
    {
        public MfeId MfeId { get; private set; }

        public MfeConfigurationName ActiveConfiguration { get; private set; }

        public ConfigurationList Versions { get; private set; }

        public MfeGlobalConfiguration(MfeId name, MfeConfigurationName active, ConfigurationList versions)
        {
            this.MfeId = name;
            this.ActiveConfiguration = active;
            this.Versions = versions;
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
        /// <param name="versions"></param>
        public void UpdateConfigurations(ConfigurationList versions)
        {
            // this.Versions = versions;
            foreach (var item in this.Versions)
            {
                versions.TryGetValue(item.Key, out var incomingVersion);
                if (incomingVersion != null)
                {
                    this.Versions[item.Key] = incomingVersion; // it will update only if the incoming version has a value
                }
            }
        }

        private static MfeConfigurationName GetFirstConfiguration(ConfigurationList versions) => versions.GetFirstConfigurationName();
    }
}
