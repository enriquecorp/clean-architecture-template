using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.domain.Bus.Event;

namespace shared.domain.Aggregate
{
    public abstract class AggregateRoot
    {
        /*
         private array $domainEvents = [];

    final public function pullDomainEvents(): array
    {
        $domainEvents       = $this->domainEvents;
        $this->domainEvents = [];

        return $domainEvents;
    }

    final protected function record(DomainEvent $domainEvent): void
    {
        $this->domainEvents[] = $domainEvent;
    }
         * */

        private List<DomainEvent> domainEvents = new();

        public List<DomainEvent> PullDomainEvents()
        {
            var events = this.domainEvents;
            this.domainEvents = new List<DomainEvent>();
            return events;
        }

        protected void Record(DomainEvent domainEvent)
        {
            this.domainEvents.Add(domainEvent);
        }
    }

}
