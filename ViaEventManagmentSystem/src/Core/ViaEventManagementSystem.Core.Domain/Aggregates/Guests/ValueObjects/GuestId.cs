using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class GuestId : ValueObject
{
    private GuestId(Guid id)
    {
        Value = id;
    }

    // EF Core will use this constructor
    private GuestId()
    {
    }

    public Guid Value { get; }

    public static Result<GuestId> Create()
    {
        return new GuestId(Guid.NewGuid());
    }

    // Factory method to create a GuestId from string
    public static Result<GuestId> Create(string id)
    {
        var canBeParsed = Guid.TryParse(id, out var guid);
        return canBeParsed
            ? Result<GuestId>.Success(new GuestId(guid))
            : Result<GuestId>.Failure(Error.AddCustomError("Problems occured during create a guid id for gueest"));
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}