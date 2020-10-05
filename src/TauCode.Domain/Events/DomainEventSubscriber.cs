using System;

namespace TauCode.Domain.Events
{
    public class DomainEventSubscriber<T> : IDomainEventSubscriber<T> where T : IDomainEvent
    {
        private readonly Action<T> _handler;

        /// <summary>
        /// Create a new domain event subscriber that executes the provided action when triggered.
        /// </summary>
        /// <param name="handler">The action that should be executed when subscriber is triggered (required)</param>
        public DomainEventSubscriber(Action<T> handler)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// <see cref="IDomainEventSubscriber{T}.HandleEvent(T)"/>
        /// </summary>
        public void HandleEvent(T domainEvent)
        {
            _handler(domainEvent);
        }
    }
}