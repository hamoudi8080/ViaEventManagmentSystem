using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class ActivateEventCommand : ICommand
{
    private ActivateEventCommand(EventId eventId)
    {
        EventId = eventId;
    }

    public EventId EventId { get; init; }

    public static Result<ActivateEventCommand> Create(string eventId)
    {
        var idResult = EventId.Create(eventId);

        return Result
            .CombineResultsInto<ActivateEventCommand>(idResult)
            .WithPayloadIfSuccess(() => new ActivateEventCommand(idResult.Payload!));
    }
}