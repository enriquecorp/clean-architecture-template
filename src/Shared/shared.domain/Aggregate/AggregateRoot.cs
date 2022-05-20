using shared.domain.Bus.Event;

namespace shared.domain.Aggregate
{
    public abstract class AggregateRoot
    {
        private List<DomainEvent> domainEvents = new();

        public List<DomainEvent> PullDomainEvents()
        {
            var events = this.domainEvents;
            this.domainEvents = new List<DomainEvent>();// events cleared
            return events;
        }

        protected void Record(DomainEvent domainEvent)
        {
            this.domainEvents.Add(domainEvent);
        }
    }

}
