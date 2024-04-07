using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Common.Factories.EventFactory;

public class ViaEventBuilder
{
    private EventId _eventId;
    private EventTitle _eventTitle;
    private EventDescription _eventDescription;
    private StartDateTime _startDateTime;
    private EndDateTime _endDateTime;
    private MaxNumberOfGuests _maxNumberOfGuests;
    private EventVisibility _eventVisibility;
    private EventStatus _eventStatus;

    public ViaEventBuilder Create(EventId eventId)
    {
        _eventId = eventId;
        return this;
    }

    public ViaEventBuilder UpdateTitle(string title)
    {
        _eventTitle = EventTitle.Create(title).Payload;
        return this;
    }

    public ViaEventBuilder UpdateDescription(string description)
    {
        _eventDescription = EventDescription.Create(description).Payload;
        return this;
    }

    public ViaEventBuilder EventTimeDuration(StartDateTime startDateTime, EndDateTime endDateTime)
    {
        _startDateTime = startDateTime;
        _endDateTime = endDateTime;
        return this;
    }

    public ViaEventBuilder MaxNumberOfGuests(MaxNumberOfGuests maxNumberOfGuests)
    {
        _maxNumberOfGuests = maxNumberOfGuests;
        return this;
    }

    public ViaEventBuilder EventVisibility(EventVisibility eventVisibility)
    {
        _eventVisibility = eventVisibility;
        return this;
    }

    public ViaEventBuilder EventStatus(EventStatus eventStatus)
    {
        _eventStatus = eventStatus;
        return this;
    }

    public ViaEvent Build()
    {
        return new ViaEvent(_eventId, _eventTitle, _eventDescription, _startDateTime, _endDateTime, _maxNumberOfGuests,
            _eventVisibility, _eventStatus);
    }
}