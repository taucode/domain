namespace TauCode.Domain.Events;

public interface IDomainEventHandler<in T> : IDomainEventHandler
    where T : class, IDomainEvent
{
    Task HandleAsync(T domainEvent, CancellationToken cancellationToken = default);
}