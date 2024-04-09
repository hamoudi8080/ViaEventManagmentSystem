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
        
        if (eventIdResult.IsSuccess && myguestId.IsSuccess) {
            return Result<DeclineInvitationCommand>.Success(new DeclineInvitationCommand(eventIdResult.Payload!, myguestId.Payload!));
        }
        
        return Result<DeclineInvitationCommand>.Failure(Error.AddCustomError("Failed to create DeclineInvitationCommand due to invalid EventId or guestId"));
    }
    
    
}