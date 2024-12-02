using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class DeclineInvitationCommand : ICommand
{
    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }
    
    private DeclineInvitationCommand(EventId eventId, GuestId guestId)
    {
        EventId = eventId;
        GuestId = guestId;
    }
    
    public static Result<DeclineInvitationCommand> Create(string eventId, string guestId) {
        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<GuestId> myguestId = GuestId.Create(guestId);
        
        var result = Result.CombineFromOthers<DeclineInvitationCommand>(eventIdResult, myguestId);
        
        return Result<DeclineInvitationCommand>.WithPayloadIfSuccess(result,
            () => new DeclineInvitationCommand(eventIdResult.Payload!, myguestId.Payload!));
    }
    
    
}