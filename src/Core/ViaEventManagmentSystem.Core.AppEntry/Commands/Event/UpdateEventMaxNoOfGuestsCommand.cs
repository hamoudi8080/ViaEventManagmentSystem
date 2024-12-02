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
        
        var combinedResult = Result.CombineFromOthers<UpdateEventMaxNoOfGuestsCommand>(idResult, maxNoOfGuestsResult);
        
        return Result<UpdateEventMaxNoOfGuestsCommand>.WithPayloadIfSuccess(combinedResult,
            () => new UpdateEventMaxNoOfGuestsCommand(idResult.Payload, maxNoOfGuestsResult.Payload));
    }
    
}