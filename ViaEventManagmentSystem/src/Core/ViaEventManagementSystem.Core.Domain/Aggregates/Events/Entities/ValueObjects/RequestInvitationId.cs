using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;

public class RequestInvitationId : ValueObject
{
    public RequestInvitationId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    public static Result<RequestInvitationId> Create()
    {
        return new RequestInvitationId(Guid.NewGuid());
    }

    // Factory method to create a GuestId from string
    public static Result<RequestInvitationId> Create(string id)
    {
        var canBeParsed = Guid.TryParse(id, out var guid);
        return canBeParsed
            ? Result<RequestInvitationId>.Success(new RequestInvitationId(guid))
            : Result<RequestInvitationId>.Failure(
                Error.AddCustomError("Problems occured during create a guid id for gueest"));
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}