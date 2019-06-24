using System;

namespace TauCode.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccuredOn { get; }
    }
}
