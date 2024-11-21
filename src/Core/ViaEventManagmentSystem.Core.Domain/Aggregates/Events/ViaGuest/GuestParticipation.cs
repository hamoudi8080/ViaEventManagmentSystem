using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.ViaGuest;

public class GuestParticipation : Entity<EventId>
{
    internal GuestId GuestId { get; }
    internal EventId EventId { get; }

    private GuestParticipation(GuestId guestId, EventId eventId)
    {
        GuestId = guestId ?? throw new ArgumentNullException(nameof(guestId));
        EventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
    }

    public static GuestParticipation Create(GuestId guestId, EventId eventId)
    {
        return new GuestParticipation(guestId, eventId);
    }

    private GuestParticipation()
    {
    }
}