using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class MakeEventPublicCommand : ICommand
{
    public EventId EventId { get; init; }
    public EventVisibility EventVisibility { get; init; }

    private MakeEventPublicCommand(EventId eventId)
    {
        EventId = eventId;
        EventVisibility = EventVisibility.Public;
    }

    public static Result<MakeEventPublicCommand> Create(string eventId)
    {
        Result<EventId> idResult = EventId.Create(eventId);
        var result = Result.CombineResultsInto<MakeEventPublicCommand>(idResult)
            .WithPayloadIfSuccess(() => new MakeEventPublicCommand(idResult.Payload!));
        return result;
    }
}