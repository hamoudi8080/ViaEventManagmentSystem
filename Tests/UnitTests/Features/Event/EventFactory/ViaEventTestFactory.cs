using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
 

namespace UnitTests.Features.Event.EventFactory;

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
        var id = EventId.Create();
        var title = EventTitle.Create("Event sport title").Payload;
        var description = EventDescription.Create(" training event description").Payload;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now.AddHours(3);
        var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
        EventVisibility eventVisibility = EventVisibility.Public;
        EventStatus eventStatus = EventStatus.Ready;

        return ViaEvent.Create(id.Payload, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
            eventStatus).Payload;
    }

    public static ViaEvent CreateActiveEvent()
    {
        var id = EventId.Create();
        return ViaEvent.Create(id.Payload, eventStatus: EventStatus.Active).Payload;
    }
    
    
    public static ViaEvent CancelledEvent()
    {
        var id = EventId.Create();
        return ViaEvent.Create(id.Payload, eventStatus: EventStatus.Cancelled).Payload;
    }
    
}