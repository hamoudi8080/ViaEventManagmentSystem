using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;

public class InvitationRequest : Entity<RequestInvitationId>
{
    internal RequestInvitationId _Id { get; private set; }
    internal EventId _EventId { get; private set; }
    internal GuestId _GuestId { get; private set; }
    internal InvitationStatus _Status { get; private set; }

    public InvitationRequest(RequestInvitationId id, EventId eventId, GuestId guestId)
    {
        _Id = id;
        _EventId = eventId;
        _GuestId = guestId;
        _Status = InvitationStatus.Pending;
    }

    public static Result<InvitationRequest> Create(RequestInvitationId id, EventId EeventId, GuestId GguestId)
    {
        return Result<InvitationRequest>.Success(new InvitationRequest( id, EeventId,
            GguestId));
    }


    public void Approve()
    {
        _Status = InvitationStatus.Accepted;
    }

    public void Deny()
    {
        _Status = InvitationStatus.Declined;
    }
}