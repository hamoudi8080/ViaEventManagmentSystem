using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class DeclineInvitationCommand : ICommand
{
    private DeclineInvitationCommand(EventId eventId, GuestId guestId)
    {
        EventId = eventId;
        GuestId = guestId;
    }

    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }

    public static Result<DeclineInvitationCommand> Create(string eventId, string guestId)
    {
        var eventIdResult = EventId.Create(eventId);
        var myguestId = GuestId.Create(guestId);
        var result = Result.CombineResultsInto<DeclineInvitationCommand>(eventIdResult, myguestId)
            .WithPayloadIfSuccess(() => new DeclineInvitationCommand(eventIdResult.Payload!, myguestId.Payload!));
        return result;
    }
}