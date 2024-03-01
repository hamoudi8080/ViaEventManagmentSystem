using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class GuestId : ValueObject
{
    public Guid Value;
    
    
    public GuestId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("GuestId cannot be empty.");

        Value = id;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}