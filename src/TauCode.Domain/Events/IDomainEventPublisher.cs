namespace TauCode.Domain.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    void AddHandler(IDomainEventHandler domainEventHandler);

    bool RemoveHandler(IDomainEventHandler domainEventHandler);
}