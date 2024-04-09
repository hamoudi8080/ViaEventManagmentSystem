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

        if (idResult.IsSuccess) {
            return Result<MakeEventPublicCommand>.Success(new MakeEventPublicCommand(idResult.Payload!));
        }
        return Result<MakeEventPublicCommand>.Failure(Error.AddCustomError("Failed to create MakeEventPublicCommand due to invalid EventId"));
    }
}