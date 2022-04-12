using shared.domain.Bus.Event;

namespace MfeGlobalConfigurations.Domain
{
    public sealed class GlobalActiveConfigurationChangedDomainEvent : DomainEvent
    {
        public string ActiveConfiguration { get; }

        public GlobalActiveConfigurationChangedDomainEvent(string mfeId, string activeConfiguration, string? eventId = null, string? occurredOn = null) : base(mfeId, eventId, occurredOn)
        {
            this.ActiveConfiguration = activeConfiguration;
        }
        public override string EventName()
        {
            return "global-active-configuration.changed";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new GlobalActiveConfigurationChangedDomainEvent(aggregateId, body[nameof(this.ActiveConfiguration)], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>{
                {nameof(this.ActiveConfiguration), this.ActiveConfiguration},
            };
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not GlobalActiveConfigurationChangedDomainEvent item)
            {
                return false;
            }

            return this.AggregateId.Equals(item.AggregateId) && this.ActiveConfiguration.Equals(item.ActiveConfiguration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.ActiveConfiguration);
        }
    }
}
