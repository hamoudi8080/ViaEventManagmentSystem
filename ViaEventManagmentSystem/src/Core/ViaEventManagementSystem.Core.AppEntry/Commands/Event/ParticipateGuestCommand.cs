using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class ParticipateGuestCommand : ICommand
{
    private ParticipateGuestCommand(EventId eventId, GuestId guestId)
    {
        EventId = eventId;
        GuestId = guestId;
    }

    public EventId EventId { get; init; }
    public GuestId GuestId { get; init; }


    public static Result<ParticipateGuestCommand> Create(string eventId, string guestId)
    {
        var idResult = EventId.Create(eventId);
        var guestResult = GuestId.Create(guestId);


        var result = Result.CombineResultsInto<ParticipateGuestCommand>(idResult, guestResult);

        return result.WithPayloadIfSuccess(() => new ParticipateGuestCommand(idResult.Payload!, guestResult.Payload!));
    }
}