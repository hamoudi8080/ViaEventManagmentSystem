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
        
        if (idResult.IsSuccess) {
            return Result<ActivateEventCommand>.Success(new ActivateEventCommand(idResult.Payload!));
        }
        
        return Result<ActivateEventCommand>.Failure(Error.AddCustomError("Failed to create ActivateEventCommand due to invalid EventId"));
    }
    
}