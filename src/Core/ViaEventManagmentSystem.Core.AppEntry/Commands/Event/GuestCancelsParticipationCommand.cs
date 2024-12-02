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

        var result = Result.CombineFromOthers<GuestCancelsParticipationCommand>(eventIdResult, myguestId);
        
        return Result<GuestCancelsParticipationCommand>.WithPayloadIfSuccess(result,
            () => new GuestCancelsParticipationCommand(eventIdResult.Payload!, myguestId.Payload!));
        
         
    }
    
}