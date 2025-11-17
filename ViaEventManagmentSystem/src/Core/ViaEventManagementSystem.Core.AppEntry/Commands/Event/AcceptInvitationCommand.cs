using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class AcceptInvitationCommand : ICommand
{
    private AcceptInvitationCommand(EventId eventId, GuestId id)
    {
        EventId = eventId;
        GuestId = id;
    }

    public EventId EventId { get; init; }

    public GuestId GuestId { get; init; }


    public static Result<AcceptInvitationCommand> Create(string eventId, string guestId)
    {
        var eventIdResult = EventId.Create(eventId);
        var guestIdResult = GuestId.Create(guestId);

        return Result.CombineResultsInto<AcceptInvitationCommand>(eventIdResult, guestIdResult)
            .WithPayloadIfSuccess(() =>
                new AcceptInvitationCommand(eventIdResult.Payload!, guestIdResult.Payload!));
    }
}