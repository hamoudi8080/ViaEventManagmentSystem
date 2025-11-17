using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;

public class Invitation : Entity<InvitationId>
{
    // EF Core will use this constructor
    private Invitation()
    {
    }

    private Invitation(EventId eventId, InvitationId id, GuestId guestId) : base(id)
    {
        _EventId = eventId;
        _GuestId = guestId;
        _InvitationStatus = InvitationStatus.Pending;
    }

    internal InvitationStatus _InvitationStatus { get; private set; }
    internal EventId _EventId { get; private set; }
    internal GuestId _GuestId { get; private set; }

    public static Result<Invitation> Create(EventId eventId, GuestId guestId)
    {
        return new Invitation(eventId, InvitationId.Create().Payload, guestId);
    }

    public Result Accept()
    {
        _InvitationStatus = InvitationStatus.Accepted;

        return Result.Success();
    }

    public Result Decline()
    {
        _InvitationStatus = InvitationStatus.Declined;

        return Result.Success();
    }
}