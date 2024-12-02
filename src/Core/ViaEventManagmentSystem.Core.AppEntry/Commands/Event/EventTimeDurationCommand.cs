using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class EventTimeDurationCommand : ICommand
{
    public EventId EventId { get; init; }
    public StartDateTime StartDateTime { get; init; }
    public EndDateTime EndDateTime { get; init; }
    
    private EventTimeDurationCommand(EventId eventId, StartDateTime startDateTime, EndDateTime endDateTime) {
        EventId = eventId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }
    
    public static Result<EventTimeDurationCommand> Create(string eventId, DateTime startDateTime, DateTime endDateTime) {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<StartDateTime> startDateTimeResult = StartDateTime.Create(startDateTime);
        Result<EndDateTime> endDateTimeResult = EndDateTime.Create(endDateTime);
         
        var result = Result.CombineFromOthers<EventTimeDurationCommand>(idResult, startDateTimeResult, endDateTimeResult);
        
        return Result<EventTimeDurationCommand>.WithPayloadIfSuccess(result,
            () => new EventTimeDurationCommand(idResult.Payload!, startDateTimeResult.Payload!, endDateTimeResult.Payload!));
    }
    
    
}