namespace TauCode.Domain.Events;

public interface IDomainEventSubscriber<in T> where T : IDomainEvent
{
    /// <summary>
    /// Handle the event.
    /// </summary>
    /// <param name="domainEvent">The event to handle</param>
    void HandleEvent(T domainEvent);
}