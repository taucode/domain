namespace TauCode.Domain.Events;

public abstract class DomainEventHandler<T> : IDomainEventHandler<T>
    where T : class, IDomainEvent
{
    public abstract Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);

    public virtual bool CanHandle(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }

        return domainEvent.GetType() == typeof(T);
    }

    public async Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        if (!this.CanHandle(domainEvent))
        {
            throw new InvalidOperationException(); // todo: meaningful message
        }

        var castEvent = (T)domainEvent;
        await this.HandleAsync(castEvent, cancellationToken);
    }
}