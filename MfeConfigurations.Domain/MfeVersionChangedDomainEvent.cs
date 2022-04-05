using shared.domain.Bus.Event;

namespace MfeConfigurations.Domain
{
    public sealed class MfeVersionChangedDomainEvent : DomainEvent
    {
        public string ConfigurationName { get; }
        public string Version { get; }

        public MfeVersionChangedDomainEvent(string id, string configurationName, string version, string? eventId = null, string? occurredOn = null) : base(id, eventId, occurredOn)
        {
            this.ConfigurationName = configurationName;
            this.Version = version;
        }
        public override string EventName()
        {
            return "mfe-configuration-version.changed";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new MfeVersionChangedDomainEvent(aggregateId, body[nameof(this.ConfigurationName)], body[nameof(this.Version)], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>{
                {nameof(this.ConfigurationName), this.ConfigurationName },
                {nameof(this.Version), this.Version},
            };
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not MfeTenantConfigurationCreatedDomainEvent item)
            {
                return false;
            }

            return this.AggregateId.Equals(item.AggregateId) && this.ConfigurationName.Equals(item.Configurations) && this.Version.Equals(item.ActiveConfiguration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.ConfigurationName, this.Version);
        }
    }
}
