using System;

namespace TauCode.Domain.Identities
{
    public interface IId
    {
        Guid Id { get; }
    }
}
