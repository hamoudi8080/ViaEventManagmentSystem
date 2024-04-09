using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class MakeEventReadyCommand : ICommand
{
    public EventId EventId { get; init; }
    
    private MakeEventReadyCommand(EventId eventId) {
        EventId = eventId;
    }
    
     
    public static Result<MakeEventReadyCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);

        if (idResult.IsSuccess) {
            return Result<MakeEventReadyCommand>.Success(new MakeEventReadyCommand(idResult.Payload!));
        }
        return Result<MakeEventReadyCommand>.Failure(idResult.Error);
    }
}