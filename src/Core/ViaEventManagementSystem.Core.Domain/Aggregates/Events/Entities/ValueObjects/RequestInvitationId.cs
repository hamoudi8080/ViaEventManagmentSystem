using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;

public class RequestInvitationId : ValueObject
{

    public Guid Value { get; }

    public RequestInvitationId(Guid id)
    {
        Value = id;
    }

    public static Result<RequestInvitationId> Create()
    {
        return new RequestInvitationId(Guid.NewGuid());
    }

    // Factory method to create a GuestId from string
    public static Result<RequestInvitationId> Create(string id)
    {
        bool canBeParsed = Guid.TryParse(id, out Guid guid);
        return canBeParsed
            ? Result<RequestInvitationId>.Success(new RequestInvitationId(guid))
            : Result<RequestInvitationId>.Failure(Error.AddCustomError("Problems occured during create a guid id for gueest"));
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}