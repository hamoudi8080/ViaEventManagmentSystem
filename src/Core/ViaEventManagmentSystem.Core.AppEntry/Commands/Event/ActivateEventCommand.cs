using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class ActivateEventCommand : ICommand
{
    public EventId EventId { get; init; }
    
    private ActivateEventCommand(EventId eventId) {
        EventId = eventId;
    }
    
    public static Result<ActivateEventCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);
        
        var result = Result.CombineFromOthers<ActivateEventCommand>(idResult);
        
        return Result<ActivateEventCommand>.WithPayloadIfSuccess(result,
            () => new ActivateEventCommand(idResult.Payload!));
    }
    
}