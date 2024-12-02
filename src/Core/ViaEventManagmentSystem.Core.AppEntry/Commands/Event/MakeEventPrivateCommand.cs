using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class MakeEventPrivateCommand : ICommand
{
    public EventId EventId { get; init; }
    public EventVisibility EventVisibility { get; init; }
    
    private MakeEventPrivateCommand(EventId eventId) {
        EventId = eventId;
    }
    
    public static Result<MakeEventPrivateCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);
        
        var result = Result.CombineFromOthers<MakeEventPrivateCommand>(idResult);
        
        return Result<MakeEventPrivateCommand>.WithPayloadIfSuccess(result,
            () => new MakeEventPrivateCommand(idResult.Payload!));
    }
    
  
    
}