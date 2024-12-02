using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class MakeEventPublicCommand : ICommand
{
    public EventId EventId { get; init; }
    public EventVisibility EventVisibility { get; init; } // Removed underscore prefix

    private MakeEventPublicCommand(EventId eventId) {
        EventId = eventId;
    }

    public static Result<MakeEventPublicCommand> Create(string eventId) {
        Result<EventId> idResult = EventId.Create(eventId);

        var result = Result.CombineFromOthers<MakeEventPublicCommand>(idResult);
        
        return Result<MakeEventPublicCommand>.WithPayloadIfSuccess(result,
            () => new MakeEventPublicCommand(idResult.Payload!));
    }
}