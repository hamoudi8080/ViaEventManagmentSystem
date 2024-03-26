using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Common.Factories.EventFactory;

public class ViaEventTestFactory
{
    public static ViaEvent CreateEvent()
    {
        var id = EventId.Create();
        var createEvent = ViaEvent.Create(id.Payload);
        return createEvent.Payload;
    }

    public static ViaEvent ReadyEvent()
    {
        var id = EventId.Create().Payload;
        var title = EventTitle.Create("Event sport title").Payload;
        var description = EventDescription.Create("Training event description").Payload;
        var startDate = StartDateTime.Create(DateTime.Now).Payload;
        var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
        var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
        var eventVisibility = EventVisibility.Public;
        var eventStatus = EventStatus.Ready;

        return ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
            eventStatus).Payload;
    }
    public static ViaEvent PrivateEvent()
    {
        var id = EventId.Create().Payload;
        var title = EventTitle.Create("Event sport title").Payload;
        var description = EventDescription.Create("Training event description").Payload;
        var startDate = StartDateTime.Create(DateTime.Now).Payload;
        var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
        var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
        var eventVisibility = EventVisibility.Private;
        var eventStatus = EventStatus.Ready;

        return ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
            eventStatus).Payload;
    }

    public static ViaEvent DraftEvent()
    {
        var id = EventId.Create().Payload;
        var title = EventTitle.Create("Event sport title").Payload;
        var description = EventDescription.Create("Training event description").Payload;
        var startDate = StartDateTime.Create(DateTime.Now.AddHours(1)).Payload;
        var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
        var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
        var eventVisibility = EventVisibility.Public;
        var eventStatus = EventStatus.Draft;

        return ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
            eventStatus).Payload;
    }

    public static ViaEvent DraftEvent(DateTime startDateTime)
    {
        var id = EventId.Create().Payload;
        var title = EventTitle.Create("Event sport title").Payload;
        var description = EventDescription.Create("Training event description").Payload;
        var startDate = StartDateTime.Create(startDateTime).Payload; // Use custom start date/time
        var endDate = EndDateTime.Create(startDateTime.AddHours(3)).Payload; // End time is set relative to start time
        var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
        var eventVisibility = EventVisibility.Public;
        var eventStatus = EventStatus.Draft;

        return ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
            eventStatus).Payload;
    }


    public static ViaEvent CreateActiveEvent()
    {
      
        var id = EventId.Create();
        var maxNumberOfGuests = MaxNumberOfGuests.Create(10);
        return ViaEvent.Create(id.Payload, maxNumberOfGuests: maxNumberOfGuests.Payload,
            eventStatus: EventStatus.Active).Payload;
    }
    public static ViaEvent CreateActiveEventFullGuest()
    {
      
        var id = EventId.Create();
        var maxNumberOfGuests = MaxNumberOfGuests.Create(5);
        return ViaEvent.Create(id.Payload, maxNumberOfGuests: maxNumberOfGuests.Payload,
            eventStatus: EventStatus.Active).Payload;
    }

    public static ViaEvent CancelledEvent()
    {
        var id = EventId.Create();
        return ViaEvent.Create(id.Payload, eventStatus: EventStatus.Cancelled).Payload;
    }
}