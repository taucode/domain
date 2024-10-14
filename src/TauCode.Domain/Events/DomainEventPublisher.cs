namespace TauCode.Domain.Events;

// todo regions
public class DomainEventPublisher : IDomainEventPublisher
{
    /// <summary>
    /// Stores the current domain event publisher in an AsyncLocal so that this can be static and globally available per request, 
    /// and still support async methods.
    /// </summary>
    private static readonly AsyncLocal<IDomainEventPublisher> CurrentStorage = new();

    /// <summary>
    /// Get the current domain event publisher. Each call context (e.g. thread) will get its own instance.
    /// Remember that call contexts are copied from the parent thread, 
    /// so DO NOT use this property in the main thread, unless the whole application should have the same instance.
    /// </summary>
    public static IDomainEventPublisher Current
    {
        get
        {
            var domainEventPublisher = CurrentStorage.Value;

            if (domainEventPublisher == null)
            {
                domainEventPublisher = new DomainEventPublisher();
                CurrentStorage.Value = domainEventPublisher;
            }

            return domainEventPublisher;
        }
    }

    /// <summary>
    /// Dispose the current domain event publisher.
    /// </summary>
    public static void Dispose()
    {
        CurrentStorage.Value = null!;
    }

    private readonly HashSet<IDomainEventHandler> _handlers = new();

    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        // todo: clever dispatching
        foreach (var handler in _handlers)
        {
            if (handler.CanHandle(domainEvent))
            {
                await handler.HandleAsync(domainEvent, cancellationToken); // todo: try/catch here
            }
        }
    }

    public void AddHandler(IDomainEventHandler domainEventHandler)
    {
        if (domainEventHandler == null)
        {
            throw new ArgumentNullException(nameof(domainEventHandler));
        }

        if (_handlers.Contains(domainEventHandler))
        {
            throw new InvalidOperationException(); // todo message
        }

        _handlers.Add(domainEventHandler);
    }

    public bool RemoveHandler(IDomainEventHandler domainEventHandler)
    {
        if (domainEventHandler == null)
        {
            throw new ArgumentNullException(nameof(domainEventHandler));
        }

        return _handlers.Remove(domainEventHandler);
    }
}