using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Bus.Event;

namespace Shared.Infrastructure.Bus
{
    public class InMemoryApplicationEventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;

        public InMemoryApplicationEventBus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Publish(List<DomainEvent> events)
        {
            if (events == null)
            {
                return;
            }

            using var scope = this.serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var subscribers = GetSubscribers(@event, scope);

                foreach (var subscriber in subscribers)
                {
                    if (subscriber is not null)
                    {
                        await ((IDomainEventSubscriberBase)subscriber).On(@event);
                    }
                }
            }
        }

        private static IEnumerable<object?> GetSubscribers(DomainEvent @event, IServiceScope scope)
        {
            var eventType = @event.GetType();
            var subscriberType = typeof(IDomainEventSubscriber<>).MakeGenericType(eventType);
            return scope.ServiceProvider.GetServices(subscriberType);
        }
        //private static IEnumerable<T> GetSubscribers<T>(DomainEvent @event, IServiceScope scope) where T : DomainEvent
        //{
        //    var eventType = @event.GetType();
        //    var subscriberType = typeof(IDomainEventSubscriber<>).MakeGenericType(eventType);
        //    return scope.ServiceProvider.GetServices<T>();
        //}
    }
}
