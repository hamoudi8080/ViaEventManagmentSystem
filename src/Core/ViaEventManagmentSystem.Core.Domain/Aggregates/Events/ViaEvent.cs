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
    internal MaxNumberOfGuests MaxNumberOfGuests { get; }
    internal EventVisibility EventVisibility { get; }

    public ViaEvent( EventTitle eventTitle, EventDescription description, DateTime startDateTime, DateTime endDateTime,MaxNumberOfGuests maxNumberOfGuests, EventVisibility eventVisibility)
    {
        GuestId = GuestId.Create();
        MaxNumberOfGuests = maxNumberOfGuests;
        EventTitle = eventTitle;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        EventVisibility = eventVisibility;
    }

    public static Result<ViaEvent> Create(EventTitle title, EventDescription description, DateTime startDateTime, DateTime endDateTime, MaxNumberOfGuests maxNumberOfGuests, EventVisibility eventVisibility)
    {
        return new ViaEvent(title,description, startDateTime,endDateTime,maxNumberOfGuests,eventVisibility);
    }
}