using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class CreateEventCommand : ICommand{ 
    
    public string EventName { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public int MaxNumberOfGuests { get; private set; }
    public EventVisibility EventVisibility { get; private set; }

    private CreateEventCommand() { }

    public static Result<CreateEventCommand> Create(string eventName, DateTime startDateTime, DateTime endDateTime, int maxNumberOfGuests, EventVisibility eventVisibility)
    {
        // Add validation logic here if necessary
        return new CreateEventCommand
        {
            EventName = eventName,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            MaxNumberOfGuests = maxNumberOfGuests,
            EventVisibility = eventVisibility
        };
    }
}