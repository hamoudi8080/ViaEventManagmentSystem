using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class EventTimeDurationCommand : ICommand
{
    private EventTimeDurationCommand(EventId eventId, StartDateTime startDateTime, EndDateTime endDateTime)
    {
        EventId = eventId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public EventId EventId { get; init; }
    public StartDateTime StartDateTime { get; init; }
    public EndDateTime EndDateTime { get; init; }

    public static Result<EventTimeDurationCommand> Create(string eventId, DateTime startDateTime, DateTime endDateTime)
    {
        var idResult = EventId.Create(eventId);
        var startDateTimeResult = StartDateTime.Create(startDateTime);
        var endDateTimeResult = EndDateTime.Create(endDateTime);

     
        var result = Result.CombineResultsInto<EventTimeDurationCommand>(idResult, startDateTimeResult, endDateTimeResult);

      
        return result.WithPayloadIfSuccess(() => new EventTimeDurationCommand(
            idResult.Payload,
            startDateTimeResult.Payload,
            endDateTimeResult.Payload));
     
    }
}