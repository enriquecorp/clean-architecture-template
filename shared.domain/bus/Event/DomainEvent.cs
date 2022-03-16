using System;
using System.Collections.Generic;
using shared.domain.ValueObjects;

namespace shared.domain.Bus.Event
{
    public abstract class DomainEvent
    {
        public string AggregateId { get; }
        public string EventId { get; }
        public string OccurredOn { get; }

        protected DomainEvent(string aggregateId, string eventId, string occurredOn)
        {
            this.AggregateId = aggregateId;
            this.EventId = eventId ?? UuidValueObject.Random().Value;
            this.OccurredOn = occurredOn ?? DateUtils.DateToString(DateTime.Now);
        }

        protected DomainEvent()
        {
        }

        public abstract string EventName();
        public abstract Dictionary<string, string> ToPrimitives();

        public abstract DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
            string occurredOn);
    }
}
