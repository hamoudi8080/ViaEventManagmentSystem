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