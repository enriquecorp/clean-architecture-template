using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.domain.Bus.Event
{
    public interface IEventBus
    {
        public Task Publish(List<DomainEvent> events);
    }
}
