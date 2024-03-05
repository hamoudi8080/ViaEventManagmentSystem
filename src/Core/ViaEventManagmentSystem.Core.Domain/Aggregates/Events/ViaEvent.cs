using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public class ViaEvent : Aggregate<EventId>
{
    internal GuestId GuestId { get; }
    internal EventTitle EventTitle { get; }
    internal EventDescription Description { get; }
    internal DateTime StartDateTime { get; }
    internal DateTime EndDateTime { get; }
    internal int MaxNumberOfGuests;
    internal EventVisibility EventVisibility { get; }





}