using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class GuestCancelsParticipationCommand : ICommand
{
    private GuestCancelsParticipationCommand(EventId eventId, GuestId id)
    {
        EventId = eventId;
        GuestId = id;
    }

    public EventId EventId { get; set; }
    public GuestId GuestId { get; set; }

    public static Result<GuestCancelsParticipationCommand> Create(string eventId, string guestId)
    {
        var eventIdResult = EventId.Create(eventId);
        var myguestId = GuestId.Create(guestId);

        var result = Result.CombineResultsInto<GuestCancelsParticipationCommand>(eventIdResult, myguestId)
            .WithPayloadIfSuccess(() =>
                new GuestCancelsParticipationCommand(eventIdResult.Payload!, myguestId.Payload!));
        return result;
    }
}