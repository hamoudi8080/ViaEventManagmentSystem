using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class MakeEventReadyCommand : ICommand
{
    public EventId EventId { get;}
    
    private MakeEventReadyCommand(EventId eventId) {
        EventId = eventId;
    }
    
     
    public static Result<MakeEventReadyCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);
        
        /*
        var result = Result.CombineFromOthers<MakeEventReadyCommand>(idResult);
        
        return Result<MakeEventReadyCommand>.WithPayloadIfSuccess(result,
            () => new MakeEventReadyCommand(idResult.Payload!));
        */
        return null;
        
    }
}