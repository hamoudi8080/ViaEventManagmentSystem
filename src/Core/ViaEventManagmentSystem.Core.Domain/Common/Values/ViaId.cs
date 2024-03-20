using ViaEventManagmentSystem.Core.Domain.Common.Bases;

namespace ViaEventManagmentSystem.Core.Domain.Common.Values;

public abstract class ViaId: ValueObject
{
    public Guid Value { get; private init; }
    
    protected ViaId(Guid value)
    {
        Value = value;
    }
}