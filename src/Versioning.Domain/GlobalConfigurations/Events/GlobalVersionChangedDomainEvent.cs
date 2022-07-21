using Shared.Domain.Bus.Event;

namespace Versioning.Domain.GlobalConfigurations.Events
{
    public sealed class GlobalVersionChangedDomainEvent : DomainEvent
    {
        public string ConfigurationName { get; }
        public string VersionUrl { get; }

        public GlobalVersionChangedDomainEvent(string mfeId, string configurationName, string versionUrl, string? eventId = null, string? occurredOn = null) : base(mfeId, eventId, occurredOn)
        {
            this.ConfigurationName = configurationName;
            this.VersionUrl = versionUrl;
        }
        public override string EventName()
        {
            return "mfe-global-configuration-versionurl.changed";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new GlobalVersionChangedDomainEvent(aggregateId, body[nameof(this.ConfigurationName)], body[nameof(this.VersionUrl)], eventId, occurredOn);
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

            if (obj is not GlobalVersionChangedDomainEvent item)
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
