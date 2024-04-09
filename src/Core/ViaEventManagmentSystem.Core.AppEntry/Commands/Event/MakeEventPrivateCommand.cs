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
        
        if (idResult.IsSuccess) {
            return Result<MakeEventPrivateCommand>.Success(new MakeEventPrivateCommand(idResult.Payload!));
        }
        return Result<MakeEventPrivateCommand>.Failure(Error.AddCustomError(idResult.ErrorMessage.ToString()));
    }
    
  
    
}