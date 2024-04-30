using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class CreateEventCommand : ICommand{


public ViaEvent ViaEvent { get; init; }
    private CreateEventCommand(ViaEvent eEvent)
    {
        ViaEvent = eEvent;
    }

    public static Result<CreateEventCommand> Create(string eventId,string eventTitle ,DateTime startDateTime, DateTime endDateTime, int maxNumberOfGuests , string eventDescription)
    {
        
        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<EventTitle> eventTitleResult = EventTitle.Create(eventTitle);
        Result<EventDescription> eventDescriptionResult = EventDescription.Create(eventDescription);
        Result<StartDateTime> startDateTimeResult = StartDateTime.Create(startDateTime);
        Result<EndDateTime> endDateTimeResult = EndDateTime.Create(endDateTime);
        Result<MaxNumberOfGuests> maxNumberOfGuestsResult = MaxNumberOfGuests.Create(maxNumberOfGuests);
        Result<EventVisibility> eventVisibilityResult = EventVisibility.Public;
        Result<EventStatus> eventStatus = EventStatus.Draft;

        if (eventTitleResult.IsSuccess && startDateTimeResult.IsSuccess && endDateTimeResult.IsSuccess && maxNumberOfGuestsResult.IsSuccess && eventVisibilityResult.IsSuccess && eventDescriptionResult.IsSuccess)
        {
            Result<ViaEvent> viaEvent = ViaEvent.Create(eventIdResult.Payload,eventTitleResult.Payload,eventDescriptionResult.Payload, startDateTimeResult.Payload, endDateTimeResult.Payload, maxNumberOfGuestsResult.Payload, eventVisibilityResult.Payload, eventStatus.Payload);
            
            
            return Result<CreateEventCommand>.Success(new CreateEventCommand(viaEvent.Payload!));
        }

        return Result<CreateEventCommand>.Failure(Error.AddCustomError("Failed to create CreateEventCommand due to invalid EventName, StartDateTime, EndDateTime, MaxNumberOfGuests, EventVisibility or EventDescription"));
    }
    
}