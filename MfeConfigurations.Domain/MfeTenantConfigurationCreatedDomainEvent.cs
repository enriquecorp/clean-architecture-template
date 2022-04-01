using shared.domain.Bus.Event;

namespace MfeConfigurations.Domain
{
    public sealed class MfeTenantConfigurationCreatedDomainEvent : DomainEvent
    {
        public MfeTenantConfigurationCreatedDomainEvent(string id, string configurations, string activeConfiguration, string eventId=null, string occurredOn=null) :base(id, eventId, occurredOn)
        {

        }
        public override string EventName()
        {
            return "mfe-tenant-configuration.created";
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new MfeTenantConfigurationCreatedDomainEvent(aggregateId, body["configurations"], body["activeConfiguration"], eventId, occurredOn);
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            throw new NotImplementedException();
        }
    }
}
