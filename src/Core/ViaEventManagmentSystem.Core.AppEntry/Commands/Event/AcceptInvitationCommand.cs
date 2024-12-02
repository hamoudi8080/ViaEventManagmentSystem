using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class AcceptInvitationCommand : ICommand
{
    public EventId EventId { get; init; }


    public GuestId GuestId { get; init; }


    private AcceptInvitationCommand(EventId eventId, GuestId id)
    {
        EventId = eventId;
        GuestId = id;
    }

    
    public static Result<AcceptInvitationCommand> Create(string eventId, string guestId) {
       

        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<GuestId> myguestId = GuestId.Create(guestId);

        var result = Result.CombineFromOthers<AcceptInvitationCommand>(eventIdResult, myguestId);
        
        return Result<AcceptInvitationCommand>.WithPayloadIfSuccess(result,
            () => new AcceptInvitationCommand(eventIdResult.Payload!, myguestId.Payload!));
    }
    

    
     
}