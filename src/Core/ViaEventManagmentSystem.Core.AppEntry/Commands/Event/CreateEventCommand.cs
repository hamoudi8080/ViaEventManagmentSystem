using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

public class CreateEventCommand : ICommand
{
    public ViaEvent ViaEvent { get; }

    private CreateEventCommand(ViaEvent viaEvent) => ViaEvent = viaEvent;

    public static Result<CreateEventCommand> Create(string eventId, string eventTitle, DateTime startDateTime, DateTime endDateTime, int maxNumberOfGuests, string eventDescription)
    {
        var eventIdResult = EventId.Create(eventId);
        var eventTitleResult = EventTitle.Create(eventTitle);
        var eventDescriptionResult = EventDescription.Create(eventDescription);
        var startDateTimeResult = StartDateTime.Create(startDateTime);
        var endDateTimeResult = EndDateTime.Create(endDateTime);
        var maxNumberOfGuestsResult = MaxNumberOfGuests.Create(maxNumberOfGuests);
        var eventVisibilityResult = Result<EventVisibility>.Success(EventVisibility.Public);
        var eventStatusResult = Result<EventStatus>.Success(EventStatus.Draft);

        var combinedResult = Result.CombineFromOthers<CreateEventCommand>(eventIdResult, eventTitleResult, eventDescriptionResult, startDateTimeResult, endDateTimeResult, maxNumberOfGuestsResult, eventVisibilityResult, eventStatusResult);

        return Result<CreateEventCommand>.WithPayloadIfSuccess(combinedResult, () =>
        {
            var viaEvent = ViaEvent.Create(
                eventIdResult.Payload,
                eventTitleResult.Payload,
                eventDescriptionResult.Payload,
                startDateTimeResult.Payload,
                endDateTimeResult.Payload,
                maxNumberOfGuestsResult.Payload,
                eventVisibilityResult.Payload,
                eventStatusResult.Payload);

            return new CreateEventCommand(viaEvent.Payload!);
        });
    }
}