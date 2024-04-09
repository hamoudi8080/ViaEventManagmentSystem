using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class UpdateDescriptionCommand : ICommand
{
    public EventDescription Description;
    public EventId EventId;
    
    private UpdateDescriptionCommand(EventId eventId, EventDescription description) {
        EventId = eventId;
        Description = description;
    }
    
    public static Result<UpdateDescriptionCommand> Create(string eventId, string description) {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<EventDescription> descriptionResult = EventDescription.Create(description);
        
    
        
        if (idResult.IsSuccess && descriptionResult.IsSuccess) {
            return Result<UpdateDescriptionCommand>.Success(new UpdateDescriptionCommand(idResult.Payload!, descriptionResult.Payload!));
        }

        return Result<UpdateDescriptionCommand>.Failure(Error.AddCustomError("Failed to create UpdateDescriptionCommand due to invalid EventId or EventTitle"));
    }
}