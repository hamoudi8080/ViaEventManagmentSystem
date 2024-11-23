using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;

public class Invitation : Entity<InvitationId>
{
    internal InvitationStatus _InvitationStatus { get; private set; }

    internal EventId _EventId { get; private set; }
    internal GuestId _GuestId { get; private set; }

    //internal InvitationId _Id { get; private set; }

    // EF Core will use this constructor
    private Invitation() 
    {
    }
    private Invitation(EventId eventId, InvitationId id, GuestId guestId) :base(id)
    {
        //Id = id;
        _EventId = eventId;
        _GuestId = guestId;
        _InvitationStatus = InvitationStatus.Pending;
    }

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