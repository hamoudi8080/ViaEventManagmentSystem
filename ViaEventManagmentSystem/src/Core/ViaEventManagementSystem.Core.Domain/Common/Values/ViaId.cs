using ViaEventManagementSystem.Core.Domain.Common.Bases;

namespace ViaEventManagementSystem.Core.Domain.Common.Values;

public abstract class ViaId : ValueObject
{
    protected ViaId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private init; }
}