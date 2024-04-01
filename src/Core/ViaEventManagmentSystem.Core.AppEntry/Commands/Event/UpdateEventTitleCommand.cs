using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class UpdateEventTitleCommand: ICommand
{
    
    public EventId EventId { get; init; }
    public EventTitle EventTitle { get; init; }

    private UpdateEventTitleCommand(EventId eventId, EventTitle eventTitle) {
        EventId = eventId;
        EventTitle = eventTitle;
    }

    public static Result<UpdateEventTitleCommand> Create(string eventId, string eventTitle) {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<EventTitle> titleResult = EventTitle.Create(eventTitle);

        if (idResult.IsSuccess && titleResult.IsSuccess) {
            return Result<UpdateEventTitleCommand>.Success(new UpdateEventTitleCommand(idResult.Payload!, titleResult.Payload!));
        }

        return Result<UpdateEventTitleCommand>.Failure(Error.AddCustomError("Failed to create UpdateEventTitleCommand due to invalid EventId or EventTitle"));
    }
    
}