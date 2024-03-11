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
    internal DateTime? _StartDateTime { get; private set; }
    internal DateTime? _EndDateTime { get; private set; }
    internal MaxNumberOfGuests? _MaxNumberOfGuests { get; private set; }
    internal EventVisibility? _EventVisibility { get; private set; }
    internal EventStatus _EventStatus { get; private set; }


    internal ViaEvent(EventId id) : base(id)
    {
        _StartDateTime = DateTime.Now;
        _EndDateTime = null;
        _EventStatus = EventStatus.Draft;
        _MaxNumberOfGuests = MaxNumberOfGuests.Create(5).Payload;
        _EventTitle = EventTitle.Create("Working Title").Payload;
        _Description = EventDescription.Create(string.Empty).Payload;
        _EventVisibility = EventVisibility.Private;
    }

    /*
     By using the null-coalescing operator (??),
     we ensure that if the parameters are not provided,
     default values are used.
    */
    public ViaEvent(EventId eventId, EventTitle? eventTitle, EventDescription? description, DateTime? startDateTime,
        DateTime? endDateTime, MaxNumberOfGuests? maxNumberOfGuests, EventVisibility? eventVisibility,
        EventStatus? eventStatus)
    {
        _eventId = eventId;
        _MaxNumberOfGuests = maxNumberOfGuests;
        _EventTitle = eventTitle;
        _Description = description;
        _StartDateTime = startDateTime ?? DateTime.Now;
        _EndDateTime = endDateTime;
        _EventVisibility = eventVisibility ?? EventVisibility.Public;
        _EventStatus = eventStatus ?? EventStatus.Draft;
    }

    public static Result<ViaEvent> Create(EventId eventId, EventTitle? title = null,
        EventDescription? description = null, DateTime? startDateTime = null,
        DateTime? endDateTime = null, MaxNumberOfGuests? maxNumberOfGuests = null,
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
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBenmodifiable));
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


        _Description = EventDescription.Create(newDescription).Payload;


        return Result.Success();
    }
}