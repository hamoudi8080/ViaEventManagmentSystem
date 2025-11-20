using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class ActivateEventCommand : ICommand
{
    public EventId EventId { get; init; }

    private ActivateEventCommand(EventId eventId)
    {
        EventId = eventId;
    }

    public static Result<ActivateEventCommand> Create(string eventId)
    {
        var idResult = EventId.Create(eventId);

        return Result
            .CombineResultsInto<ActivateEventCommand>(idResult)
            .WithPayloadIfSuccess(() => new ActivateEventCommand(idResult.Payload!));
    }
}