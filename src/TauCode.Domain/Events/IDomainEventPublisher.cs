namespace TauCode.Domain.Events
{
    public interface IDomainEventPublisher
    {
        /// <summary>
        /// Publish a domain events to all registered subscribers for that specific event, or for subscribers for all events.
        /// </summary>
        /// <typeparam name="T">The type of event to publish</typeparam>
        /// <param name="domainEvent">The event to publish</param>
        void Publish<T>(T domainEvent) where T : IDomainEvent;

        /// <summary>
        /// Subscribe to a domain event. 
        /// If <see cref="IDomainEvent"/> is specified as the type, it means all domain events should be subscribed to.
        /// </summary>
        /// <typeparam name="T">The type of the event to subscribe to</typeparam>
        /// <param name="subscriber">The subscriber of the event</param>
        void Subscribe<T>(IDomainEventSubscriber<T> subscriber) where T : IDomainEvent;

        /// <summary>
        /// Unsubscribe from an event (<see cref="IDomainEvent"/> means all events)
        /// </summary>
        /// <typeparam name="T">The type of event to unsubscribe from</typeparam>
        /// <param name="subscriber">The subscriber to unsubscribe</param>
        void Unsubscribe<T>(IDomainEventSubscriber<T> subscriber) where T : IDomainEvent;
    }
}
