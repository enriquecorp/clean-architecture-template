using shared.domain.Bus.Event;

namespace MfeConfigurations.Domain
{
    public sealed class MfeActiveConfigurationChangedDomainEvent : DomainEvent
    {
        //public string PreviousConfiguration { get; }
        public string ActiveConfiguration { get; }

        public MfeActiveConfigurationChangedDomainEvent(string id, string activeConfiguration, string? eventId = null, string? occurredOn = null) : base(id, eventId, occurredOn)
        {
            //this.PreviousConfiguration = previousConfiguration;
            this.ActiveConfiguration = activeConfiguration;
        }
        public override string EventName()
        {
            return "mfe-active-configuration.changed";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new MfeActiveConfigurationChangedDomainEvent(aggregateId, body[nameof(this.ActiveConfiguration)], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>{
                //{nameof(this.PreviousConfiguration), this.PreviousConfiguration },
                {nameof(this.ActiveConfiguration), this.ActiveConfiguration},
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

            //return this.AggregateId.Equals(item.AggregateId) && this.PreviousConfiguration.Equals(item.Configurations) && this.ActiveConfiguration.Equals(item.ActiveConfiguration);
            return this.AggregateId.Equals(item.AggregateId) && this.ActiveConfiguration.Equals(item.ActiveConfiguration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.ActiveConfiguration);
        }
    }
}
