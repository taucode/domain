namespace TauCode.Domain.Events;

public interface IDomainEventHandler
{
    bool CanHandle(IDomainEvent domainEvent);

    Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}