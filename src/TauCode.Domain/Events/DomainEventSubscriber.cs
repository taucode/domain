using System;

namespace TauCode.Domain.Events
{
    public class DomainEventSubscriber<T> : IDomainEventSubscriber<T> where T : IDomainEvent
    {
        private readonly Action<T> _handle;

        /// <summary>
        /// Create a new domain event subscriber that executes the provided action when triggered.
        /// </summary>
        /// <param name="handle">The action that should be executed when subscriber is triggered (required)</param>
        public DomainEventSubscriber(Action<T> handle)
        {
            _handle = handle ?? throw new ArgumentNullException(nameof(handle));
        }

        /// <summary>
        /// <see cref="IDomainEventSubscriber{T}.HandleEvent(T)"/>
        /// </summary>
        public void HandleEvent(T domainEvent)
        {
            this._handle(domainEvent);
        }
    }
}