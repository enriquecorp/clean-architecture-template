namespace Shared.Domain.Bus.Event
{
    public interface IEventBus
    {
        public Task Publish(List<DomainEvent> events);
    }
}
