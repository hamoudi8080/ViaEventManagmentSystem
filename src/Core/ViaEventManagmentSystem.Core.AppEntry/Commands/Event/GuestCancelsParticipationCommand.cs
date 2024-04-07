using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class GuestCancelsParticipationCommand : ICommand
{
    public EventId EventId { get; set; }
    public GuestId GuestId { get; set; }

    private GuestCancelsParticipationCommand(EventId eventId, GuestId id)
    {
        EventId = eventId;
        GuestId = id;
    }

    public static Result<GuestCancelsParticipationCommand> Create(string eventId, string guestId)
    {
        Result<EventId> eventIdResult = EventId.Create(eventId);
        Result<GuestId> myguestId = GuestId.Create(guestId);

        if (eventIdResult.IsSuccess && myguestId.IsSuccess)
        {
            return Result<GuestCancelsParticipationCommand>.Success(new GuestCancelsParticipationCommand(eventIdResult.Payload!, myguestId.Payload!));
        }
       
        return Result<GuestCancelsParticipationCommand>.Failure(Error.AddCustomError("Failed to create GuestCancelsParticipationCommand due to invalid EventId or guestId"));
        
         
    }
    
}