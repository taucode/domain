namespace TauCode.Domain.Events
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        /// <summary>
        /// Stores the current domain event publisher in an AsyncLocal so that this can be static and globally available per request, 
        /// and still support async methods.
        /// </summary>
        private static readonly AsyncLocal<IDomainEventPublisher> _current = new AsyncLocal<IDomainEventPublisher>();

        /// <summary>
        /// Get the current domain event publisher. Each call context (e.g. thread) will get its own instance.
        /// Remember that call contexts are copied from the parent thread, 
        /// so DO NOT use this property in the main thread, unless the whole application should have the same instance.
        /// </summary>
        public static IDomainEventPublisher Current
        {
            get
            {
                var domainEventPublisher = _current.Value;

                if (domainEventPublisher == null)
                {
                    domainEventPublisher = new DomainEventPublisher();
                    _current.Value = domainEventPublisher;
                }

                return domainEventPublisher;
            }
        }

        /// <summary>
        /// Dispose the current domain event publisher.
        /// </summary>
        public static void Dispose()
        {
            _current.Value = null!;
        }

        private readonly IList<object> _subscribers;

        /// <summary>
        /// Private constructor for creating a domain event publisher.
        /// The <see cref="DomainEventPublisher.Current"/> property (factory method) is the public method for getting an instance.
        /// </summary>
        private DomainEventPublisher()
        {
            _subscribers = new List<object>();
        }

        /// <summary>
        /// <see cref="IDomainEventPublisher.Publish{T}(T)"/>
        /// </summary>
        public void Publish<T>(T domainEvent) where T : IDomainEvent
        {
            foreach (var subscriber in this._subscribers)
            {
                if (subscriber is IDomainEventSubscriber<T> || subscriber is IDomainEventSubscriber<IDomainEvent>)
                {
                    ((IDomainEventSubscriber<T>)subscriber).HandleEvent(domainEvent);
                }
            }
        }

        /// <summary>
        /// <see cref="IDomainEventPublisher.Subscribe{T}(IDomainEventSubscriber{T})"/>
        /// </summary>
        public void Subscribe<T>(IDomainEventSubscriber<T> subscriber) where T : IDomainEvent
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            _subscribers.Add(subscriber);
        }

        /// <summary>
        /// <see cref="IDomainEventPublisher.Unsubscribe{T}(IDomainEventSubscriber{T})"/>
        /// </summary>
        public void Unsubscribe<T>(IDomainEventSubscriber<T> subscriber) where T : IDomainEvent
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            _subscribers.Remove(subscriber);
        }
    }
}
