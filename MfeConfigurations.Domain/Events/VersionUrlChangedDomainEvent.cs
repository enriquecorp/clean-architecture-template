using shared.domain.Bus.Event;

namespace MfeConfigurations.Domain.Events
{
    public sealed class VersionUrlChangedDomainEvent : DomainEvent
    {
        public string ConfigurationName { get; }
        public string VersionUrl { get; }

        public VersionUrlChangedDomainEvent(string id, string configurationName, string versionUrl, string? eventId = null, string? occurredOn = null) : base(id, eventId, occurredOn)
        {
            this.ConfigurationName = configurationName;
            this.VersionUrl = versionUrl;
        }
        public override string EventName()
        {
            return "mfe-tenant-configuration-versionurl.changed";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new VersionUrlChangedDomainEvent(aggregateId, body[nameof(this.ConfigurationName)], body[nameof(this.VersionUrl)], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>{
                {nameof(this.ConfigurationName), this.ConfigurationName },
                {nameof(this.VersionUrl), this.VersionUrl},
            };
        }

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not VersionUrlChangedDomainEvent item)
            {
                return false;
            }

            return this.AggregateId.Equals(item.AggregateId) && this.ConfigurationName.Equals(item.ConfigurationName) && this.VersionUrl.Equals(item.VersionUrl);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.ConfigurationName, this.VersionUrl);
        }
    }
}
