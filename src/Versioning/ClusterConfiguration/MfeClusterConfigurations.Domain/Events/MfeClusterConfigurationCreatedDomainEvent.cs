﻿using shared.domain.Bus.Event;

namespace MfeClusterConfigurations.Domain.Events
{
    public sealed class MfeClusterConfigurationCreatedDomainEvent : DomainEvent
    {
        public string Configurations { get; }
        public string ActiveConfiguration { get; }

        public MfeClusterConfigurationCreatedDomainEvent(string mfeIdTenantId, string configurations, string activeConfiguration, string? eventId = null, string? occurredOn = null) : base(mfeIdTenantId, eventId, occurredOn)
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
            return new MfeClusterConfigurationCreatedDomainEvent(aggregateId, body["configurations"], body["activeConfiguration"], eventId, occurredOn);
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

            if (obj is not MfeClusterConfigurationCreatedDomainEvent item)
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