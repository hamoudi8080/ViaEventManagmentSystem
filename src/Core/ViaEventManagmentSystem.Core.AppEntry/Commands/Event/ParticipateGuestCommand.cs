using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

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
        
        if (idResult.IsSuccess && guestResult.IsSuccess) {
            return Result<ParticipateGuestCommand>.Success(new ParticipateGuestCommand(idResult.Payload!, guestResult.Payload!));
        }

        return Result<ParticipateGuestCommand>.Failure(Error.AddCustomError("Failed to create ParticipateGuestCommand due to invalid EventId or GuestId"));
    }   
    
}