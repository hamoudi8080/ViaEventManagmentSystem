using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class InviteGuestCommand : ICommand
{
    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }

    private InviteGuestCommand(EventId eventId, GuestId guestId) {
        EventId = eventId;
        GuestId = guestId;
    }
    
    public static Result<InviteGuestCommand> Create(string eventId, string guestId) {
        var eventIdResult = EventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);
        
        var result = Result.CombineResultsInto<InviteGuestCommand>(eventIdResult, guestIdResult)
            .WithPayloadIfSuccess(() => new InviteGuestCommand(eventIdResult.Payload!, guestIdResult.Payload!));
        return result;
 
    }
    
    
    
}