using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class UpdateEventMaxNoOfGuestsCommand : ICommand
{
    
    public  EventId EventId { get; init; } 
    public  MaxNumberOfGuests MaxNoOfGuests { get; init; }  
    
    private UpdateEventMaxNoOfGuestsCommand(EventId eventId, MaxNumberOfGuests maxNoOfGuests) {
        EventId = eventId;
        MaxNoOfGuests = maxNoOfGuests;
    }
    
    public static Result<UpdateEventMaxNoOfGuestsCommand> Create(string eventId, int maxNoOfGuests) {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<MaxNumberOfGuests> maxNoOfGuestsResult = MaxNumberOfGuests.Create(maxNoOfGuests);
        
        if (idResult.IsSuccess && maxNoOfGuestsResult.IsSuccess) {
            return Result<UpdateEventMaxNoOfGuestsCommand>.Success(new UpdateEventMaxNoOfGuestsCommand(idResult.Payload!, maxNoOfGuestsResult.Payload!));
        }
        
        return Result<UpdateEventMaxNoOfGuestsCommand>.Failure(Error.AddCustomError("Failed to create UpdateEventMaxNoOfGuestsCommand due to invalid EventId or MaxNoOfGuests"));
    }
    
}