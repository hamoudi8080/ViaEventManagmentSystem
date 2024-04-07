using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class MakeEventReadyCommand : ICommand
{
    public EventId EventId { get; init; }
    
    private MakeEventReadyCommand(EventId eventId) {
        EventId = eventId;
    }
    
    //TODO: ask if what i am doing here is correct!! when i have to make event ready i do nothing here in command class. case i only expect id from user i and test his id. then from handler i just the event ready!
    public static Result<MakeEventReadyCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);

        if (idResult.IsSuccess) {
            return Result<MakeEventReadyCommand>.Success(new MakeEventReadyCommand(idResult.Payload!));
        }
        return Result<MakeEventReadyCommand>.Failure(idResult.Error);
    }
}