using System;

namespace TauCode.Domain.Events
{
    public interface IDomainEvent
    {
        string CorrelationId { get; }
        DateTimeOffset OccurredAt { get; }
    }
}
