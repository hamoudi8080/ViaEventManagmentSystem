using ViaEventManagementSystem.Core.Domain.Common.Bases;

namespace ViaEventManagementSystem.Core.Domain.Common.Values;

public class MId : ValueObject
{
    protected MId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static MId Create()
    {
        return new MId(Guid.NewGuid());
    }

    public static MId FromGuid(Guid guid)
    {
        return new MId(guid);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}