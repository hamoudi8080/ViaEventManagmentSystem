using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class UpdateEventMaxNoOfGuestsCommand : ICommand
{
    private UpdateEventMaxNoOfGuestsCommand(EventId eventId, MaxNumberOfGuests maxNoOfGuests)
    {
        EventId = eventId;
        MaxNoOfGuests = maxNoOfGuests;
    }

    public EventId EventId { get; init; }
    public MaxNumberOfGuests MaxNoOfGuests { get; init; }

    public static Result<UpdateEventMaxNoOfGuestsCommand> Create(string eventId, int maxNoOfGuests)
    {
        var idResult = EventId.Create(eventId);
        var maxNoOfGuestsResult = MaxNumberOfGuests.Create(maxNoOfGuests);

        var combinedResult = Result.CombineResultsInto<UpdateEventMaxNoOfGuestsCommand>(idResult, maxNoOfGuestsResult)
            .WithPayloadIfSuccess(() =>
                new UpdateEventMaxNoOfGuestsCommand(idResult.Payload!, maxNoOfGuestsResult.Payload!));

        return combinedResult;
    }
}