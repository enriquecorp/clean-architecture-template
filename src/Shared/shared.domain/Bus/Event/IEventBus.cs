namespace shared.domain.Bus.Event
{
    public interface IEventBus
    {
        public Task Publish(List<DomainEvent> events);
    }
}
