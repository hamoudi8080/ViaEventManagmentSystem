using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class UpdateDescriptionCommand : ICommand
{
    public EventDescription Description;
    public EventId EventId;

    private UpdateDescriptionCommand(EventId eventId, EventDescription description)
    {
        EventId = eventId;
        Description = description;
    }

    public static Result<UpdateDescriptionCommand> Create(string eventId, string description)
    {
        Result<EventId> idResult = EventId.Create(eventId);
        Result<EventDescription> descriptionResult = EventDescription.Create(description);


        var combinedResult = Result.CombineResultsInto<UpdateDescriptionCommand>(idResult, descriptionResult)
            .WithPayloadIfSuccess(() =>
                new UpdateDescriptionCommand(idResult.Payload!, descriptionResult.Payload!));


        return combinedResult;
    }
}