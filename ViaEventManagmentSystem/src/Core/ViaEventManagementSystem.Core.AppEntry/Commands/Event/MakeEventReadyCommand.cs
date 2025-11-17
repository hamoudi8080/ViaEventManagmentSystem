using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class MakeEventReadyCommand : ICommand
{
    private MakeEventReadyCommand(EventId eventId)
    {
        EventId = eventId;
    }

    public EventId EventId { get; }


    public static Result<MakeEventReadyCommand> Create(string eventId)
    {
        var idResult = EventId.Create(eventId);

        var result = Result.CombineResultsInto<MakeEventReadyCommand>(idResult);
        return result.WithPayloadIfSuccess(() => new MakeEventReadyCommand(idResult.Payload!));
    }
}