using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Event;

public class CreateEventCommand : ICommand
{
    public EventId EventId { get; }
    public EventTitle Title { get; }
    public EventDescription Description { get; }
    public StartDateTime Start { get; }
    public EndDateTime End { get; }
    public MaxNumberOfGuests MaxGuests { get; }
    public EventVisibility Visibility { get; }

    private CreateEventCommand(
        EventId eventId,
        EventTitle title,
        EventDescription description,
        StartDateTime start,
        EndDateTime end,
        MaxNumberOfGuests maxGuests,
        EventVisibility visibility)
    {
        EventId = eventId;
        Title = title;
        Description = description;
        Start = start;
        End = end;
        MaxGuests = maxGuests;
        Visibility = visibility;
    }


    public static Result<CreateEventCommand> Create(string eventId, string eventTitle, DateTime startDateTime,
        DateTime endDateTime, int maxNumberOfGuests, string eventDescription)
    {
        var eventIdResult = EventId.Create(eventId);
        var eventTitleResult = EventTitle.Create(eventTitle);
        var eventDescriptionResult = EventDescription.Create(eventDescription);
        var startDateTimeResult = StartDateTime.Create(startDateTime);
        var endDateTimeResult = EndDateTime.Create(endDateTime);
        var maxNumberOfGuestsResult = MaxNumberOfGuests.Create(maxNumberOfGuests);
        var eventVisibilityResult = Result<EventVisibility>.Success(EventVisibility.Public);
        var eventStatusResult = Result<EventStatus>.Success(EventStatus.Draft);

        var combinedResult = Result.CombineResultsInto<CreateEventCommand>(
            eventIdResult,
            eventTitleResult,
            eventDescriptionResult,
            startDateTimeResult,
            endDateTimeResult,
            maxNumberOfGuestsResult,
            eventVisibilityResult,
            eventStatusResult).WithPayloadIfSuccess(() => new CreateEventCommand(
            eventIdResult.Payload!,
            eventTitleResult.Payload!,
            eventDescriptionResult.Payload!,
            startDateTimeResult.Payload!,
            endDateTimeResult.Payload!,
            maxNumberOfGuestsResult.Payload!,
            eventVisibilityResult.Payload!));
        
        return combinedResult;
    }
}