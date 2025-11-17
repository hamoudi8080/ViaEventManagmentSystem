using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;

public class InvitationId : ValueObject
{
    public InvitationId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    public static Result<InvitationId> Create()
    {
        return new InvitationId(Guid.NewGuid());
    }

    // Factory method to create a GuestId from string
    public static Result<InvitationId> Create(string id)
    {
        var canBeParsed = Guid.TryParse(id, out var guid);
        return canBeParsed
            ? Result<InvitationId>.Success(new InvitationId(guid))
            : Result<InvitationId>.Failure(Error.AddCustomError("Problems occured during create a guid id for gueest"));
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}