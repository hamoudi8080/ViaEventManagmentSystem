using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class MakeEventPublicCommand : ICommand
{
    private MakeEventPublicCommand(EventId eventId)
    {
        EventId = eventId;
        EventVisibility = EventVisibility.Public;
    }

    public EventId EventId { get; init; }
    public EventVisibility EventVisibility { get; init; }

    public static Result<MakeEventPublicCommand> Create(string eventId)
    {
        var idResult = EventId.Create(eventId);
        var result = Result.CombineResultsInto<MakeEventPublicCommand>(idResult)
            .WithPayloadIfSuccess(() => new MakeEventPublicCommand(idResult.Payload!));
        return result;
    }
}