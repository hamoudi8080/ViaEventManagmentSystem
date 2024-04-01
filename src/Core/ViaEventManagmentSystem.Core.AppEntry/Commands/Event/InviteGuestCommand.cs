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

        if(eventIdResult.IsSuccess && guestIdResult.IsSuccess) {
            return Result<InviteGuestCommand>.Success(new InviteGuestCommand(eventIdResult.Payload!, guestIdResult.Payload!));
        }
        
        return Result<InviteGuestCommand>.Failure(Error.AddCustomError("Failed to create InviteGuestCommand due to invalid EventId or GuestId"));
    }
    
    
    
}