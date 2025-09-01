using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class MakeEventPrivateCommand : ICommand
{
    public EventId EventId { get; init; }
    public EventVisibility EventVisibility { get; init; }
    
    private MakeEventPrivateCommand(EventId eventId) {
        EventId = eventId;
        EventVisibility = EventVisibility.Private;
    }
    
    public static Result<MakeEventPrivateCommand> Create(string eventId) {
        var idResult = EventId.Create(eventId);
        return Result
            .CombineResultsInto<MakeEventPrivateCommand>(idResult)
            .WithPayloadIfSuccess(() => new MakeEventPrivateCommand(idResult.Payload!));
    }
    
  
    
}