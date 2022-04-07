namespace shared.domain.Bus.Event
{
    public interface IDomainEventSubscriber<TDomain> : IDomainEventSubscriberBase where TDomain : DomainEvent
    {
        async Task IDomainEventSubscriberBase.On(DomainEvent domainEvent)
        {
            if (domainEvent is TDomain msg)
            {
                await this.On(msg);
            }
        }

        Task On(TDomain domainEvent);
    }

    public interface IDomainEventSubscriberBase
    {
        Task On(DomainEvent domainEvent);
    }
}
