using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public class ViaEvent : Aggregate<EventId>
{
    internal EventId _eventId { get; private set; }
    internal EventTitle? _EventTitle { get; private set; }
    internal EventDescription? _Description { get; private set; }
    internal StartDateTime? _StartDateTime { get; private set; }
    internal EndDateTime? _EndDateTime { get; private set; }
    internal MaxNumberOfGuests? _MaxNumberOfGuests { get; private set; }
    internal EventVisibility? _EventVisibility { get; private set; }
    internal EventStatus _EventStatus { get; private set; }

    internal ViaEvent(EventId id) : base(id)
    {
        _StartDateTime = StartDateTime.Create(DateTime.Now).Payload;
        _EndDateTime = null;
        _EventStatus = EventStatus.Draft;
        _MaxNumberOfGuests = MaxNumberOfGuests.Create(5).Payload;
        _EventTitle = EventTitle.Create("Working Title").Payload;
        _Description = EventDescription.Create(string.Empty).Payload;
        _EventVisibility = EventVisibility.Private;
    }

    public ViaEvent(EventId eventId, EventTitle? eventTitle, EventDescription? description,
        StartDateTime? startDateTime,
        EndDateTime? endDateTime, MaxNumberOfGuests? maxNumberOfGuests, EventVisibility? eventVisibility,
        EventStatus? eventStatus)
    {
        _eventId = eventId;
        _MaxNumberOfGuests = maxNumberOfGuests;
        _EventTitle = eventTitle;
        _Description = description;
        _StartDateTime = startDateTime ?? StartDateTime.Create(DateTime.Now).Payload;
        _EndDateTime = endDateTime;
        _EventVisibility = eventVisibility ?? EventVisibility.Public;
        _EventStatus = eventStatus ?? EventStatus.Draft;
    }

    public static Result<ViaEvent> Create(EventId eventId, EventTitle? title = null,
        EventDescription? description = null, StartDateTime? startDateTime = null,
        EndDateTime? endDateTime = null, MaxNumberOfGuests? maxNumberOfGuests = null,
        EventVisibility? eventVisibility = null,
        EventStatus eventStatus = EventStatus.Draft)
    {
        return Result<ViaEvent>.Success(new ViaEvent(eventId, title, description, startDateTime, endDateTime,
            maxNumberOfGuests,
            eventVisibility, eventStatus));
    }

    public static Result<ViaEvent> Create(EventId id)
    {
        return Result<ViaEvent>.Success(new ViaEvent(id));
    }

    public Result UpdateTitle(string newTitle)
    {
        if (newTitle == null || newTitle.Length < 3 || newTitle.Length > 75)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.TitleMustBeBetween3And75Chars));
        }

        if (_EventStatus == EventStatus.Active)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.ActiveEventCanotBeModified));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        _EventTitle = EventTitle.Create(newTitle).Payload;

        if (_EventStatus == EventStatus.Ready)
        {
            _EventStatus = EventStatus.Draft; // Update event status to Draft
        }

        return Result.Success();
    }

    public Result UpdateDescription(string newDescription)
    {
        if (newDescription.Length < 0 || newDescription.Length > 250)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.DescriptionMustBeBetween0And250Chars));
        }

        if (_EventStatus == EventStatus.Ready)
        {
            _Description = EventDescription.Create(newDescription).Payload;

            _EventStatus = EventStatus.Draft;
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        if (_EventStatus == EventStatus.Active)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.ActiveEventCanotBeModified));
        }

        _Description = EventDescription.Create(newDescription).Payload;


        return Result.Success();
    }

    public Result EventTimeDuration(StartDateTime? startDateTime, EndDateTime? endDateTime)
    {
        if (startDateTime == null)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
        }

        if (_EventStatus == EventStatus.Active)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.ActiveEventCanotBeModified));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        DateTime startTime = startDateTime.Value;
        DateTime endTime = endDateTime.Value;

        // Check if start time is after end time
        if (startTime > endTime)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.StartTimeMustBeBeforeEndTime));
        }

        // Check if the hour is before 08:00 or if it's after 24:00
        if (startTime.Hour < 8)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventCannotStartBefore8Am));


        //duration of the event is less than 1
        if ((endTime - startTime).TotalHours < 1)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventDurationLessThan1Hour));
        }

        //duration of the event is longer than 10
        if ((endTime - startTime).TotalHours > 10)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventDurationGreaterThan10Hours));
        }


        _StartDateTime = startDateTime;
        _EndDateTime = endDateTime;

        // Update event status if necessary
        if (_EventStatus == EventStatus.Ready)
        {
            _EventStatus = EventStatus.Draft;
        }

        // Check if the start time is in the past
        if (startTime < DateTime.Now)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventStartTimeCannotBeInPast));
        }


        return Result.Success();
    }
}