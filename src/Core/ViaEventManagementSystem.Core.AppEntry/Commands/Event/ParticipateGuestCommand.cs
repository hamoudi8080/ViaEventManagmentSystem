using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class ParticipateGuestCommand : ICommand
{
    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }
    
    private ParticipateGuestCommand(EventId eventId, GuestId guestId) {
        EventId = eventId;
        GuestId = guestId;
    }
    
 
    public static Result<ParticipateGuestCommand> Create(string eventId, string guestId) {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<GuestId> guestResult = GuestId.Create(guestId);
        
        /*
        var result = Result.CombineFromOthers<ParticipateGuestCommand>(idResult, guestResult);
        
        return Result<ParticipateGuestCommand>.WithPayloadIfSuccess(result,
            () => new ParticipateGuestCommand(idResult.Payload!, guestResult.Payload!));
            */
        return null;
    }   
    
}