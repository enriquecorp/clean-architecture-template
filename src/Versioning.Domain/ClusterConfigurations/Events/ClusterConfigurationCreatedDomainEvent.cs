using Shared.Domain.Bus.Event;

namespace Versioning.Domain.ClusterConfigurations.Events
{
    public sealed class ClusterConfigurationCreatedDomainEvent : DomainEvent
    {
        public string Configurations { get; }
        public string ActiveConfiguration { get; }

        public ClusterConfigurationCreatedDomainEvent(string mfeIdTenantId, string configurations, string activeConfiguration, string? eventId = null, string? occurredOn = null) : base(mfeIdTenantId, eventId, occurredOn)
        {
            this.Configurations = configurations;
            this.ActiveConfiguration = activeConfiguration;
        }
        public override string EventName()
        {
            return "mfe-cluster-configuration.created";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new ClusterConfigurationCreatedDomainEvent(aggregateId, body["configurations"], body["activeConfiguration"], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>{
                {"configurations", this.Configurations },
                {"active", this.ActiveConfiguration},
            };
        }

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not ClusterConfigurationCreatedDomainEvent item)
            {
                return false;
            }

            return this.AggregateId.Equals(item.AggregateId) && this.Configurations.Equals(item.Configurations) && this.ActiveConfiguration.Equals(item.ActiveConfiguration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.Configurations, this.ActiveConfiguration);
        }
    }
}
