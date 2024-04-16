using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.ViaGuest;

public class GuestParticipation
{
    public GuestId GuestId { get; set; }
    public EventId EventId { get; set; }

    public static implicit operator GuestParticipation(GuestId guestId) => new(guestId);

    public static implicit operator GuestId(GuestParticipation reference) => reference.GuestId;
    private GuestParticipation(GuestId guestId) => GuestId = guestId;

    private GuestParticipation()
    {
    }
}