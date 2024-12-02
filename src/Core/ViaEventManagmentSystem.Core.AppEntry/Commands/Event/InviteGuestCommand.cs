using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class InviteGuestCommand : ICommand
{
    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }

    private InviteGuestCommand(EventId eventId, GuestId guestId) {
        EventId = eventId;
        GuestId = guestId;
    }
    
    public static Result<InviteGuestCommand> Create(string eventId, string guestId) {
        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<GuestId> guestIdResult = GuestId.Create(guestId);

        var result = Result.CombineFromOthers<InviteGuestCommand>(eventIdResult, guestIdResult);
        
        return Result<InviteGuestCommand>.WithPayloadIfSuccess(result,
            () => new InviteGuestCommand(eventIdResult.Payload!, guestIdResult.Payload!));
    }
    
    
    
}