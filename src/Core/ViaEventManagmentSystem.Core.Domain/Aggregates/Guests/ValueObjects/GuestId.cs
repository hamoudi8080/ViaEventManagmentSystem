using ViaEventManagmentSystem.Core.Domain.Common.Values;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class GuestId : ValueObject
{
    public Guid Value { get; }

    public GuestId(Guid id)
    {
        Value = id;
    }

    public static Result<GuestId> Create(Guid id)
    {
        return Result<GuestId>.Success(new GuestId(id));
    }

    // Factory method to create a GuestId from string
    public static Result<GuestId> Create(string id)
    {
        if (!Guid.TryParse(id, out Guid result))
            return Result<GuestId>.Failure(new Error(0, "Invalid GuestId format"));

        return Create(result);
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
