using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events;

public class ViaEvent : Aggregate<EventId>
{
    // EF Core will use this constructor
    private ViaEvent()
    {
    }

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

    internal ViaEvent(EventId eventId,
        EventTitle? eventTitle,
        EventDescription? description,
        StartDateTime? startDateTime,
        EndDateTime? endDateTime, MaxNumberOfGuests? maxNumberOfGuests, EventVisibility? eventVisibility,
        EventStatus? eventStatus) : base(eventId)
    {
        _MaxNumberOfGuests = maxNumberOfGuests;
        _EventTitle = eventTitle;
        _Description = description;
        _StartDateTime = startDateTime ?? StartDateTime.Create(DateTime.Now).Payload;
        _EndDateTime = endDateTime;
        _EventVisibility = eventVisibility ?? EventVisibility.Public;
        _EventStatus = eventStatus ?? EventStatus.Draft;
        _GuestsParticipants = new HashSet<GuestId>();
        _Invitations = new List<Invitation>();
    }

    internal EventId _eventId => Id;
    internal EventTitle? _EventTitle { get; private set; }
    internal EventDescription? _Description { get; private set; }
    internal StartDateTime? _StartDateTime { get; private set; }
    internal EndDateTime? _EndDateTime { get; private set; }
    internal MaxNumberOfGuests? _MaxNumberOfGuests { get; private set; }
    internal EventVisibility? _EventVisibility { get; private set; }
    internal EventStatus _EventStatus { get; private set; }
    internal HashSet<GuestId> _GuestsParticipants { get; }
    internal List<Invitation> _Invitations { get; }

    public static Result<ViaEvent> Create(EventId eventId,
        EventTitle? title = null,
        EventDescription? description = null,
        StartDateTime? startDateTime = null,
        EndDateTime? endDateTime = null,
        MaxNumberOfGuests? maxNumberOfGuests = null,
        EventVisibility? eventVisibility = null,
        EventStatus eventStatus = EventStatus.Draft)
    {
        var viaEvent = new ViaEvent(eventId, title, description, startDateTime, endDateTime, maxNumberOfGuests,
            eventVisibility, eventStatus);
        return Result<ViaEvent>.Success(viaEvent);
    }

    public static Result<ViaEvent> Create(EventId eventId)
    {
        return Result<ViaEvent>.Success(new ViaEvent(eventId));
    }

    public Result UpdateTitle(EventTitle title)
    {
        if (_EventStatus == EventStatus.Active)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.ActiveEventCannotBeModified));

        if (_EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeModified));

        if (_EventStatus == EventStatus.Ready) _EventStatus = EventStatus.Draft;

        _EventTitle = title;

        return Result.Success();
    }

    public Result UpdateDescription(EventDescription description)
    {
        if (_EventStatus == EventStatus.Ready) _EventStatus = EventStatus.Draft;

        if (_EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeModified));

        if (_EventStatus == EventStatus.Active)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.ActiveEventCannotBeModified));

        _Description = description;

        return Result.Success();
    }

    public Result EventTimeDuration(StartDateTime? startDateTime, EndDateTime? endDateTime)
    {
        if (startDateTime == null || endDateTime == null)
            return Result.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));

        if (_EventStatus == EventStatus.Active)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.ActiveEventCannotBeModified));

        if (_EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeModified));

        var startTime = startDateTime.Value;
        var endTime = endDateTime.Value;

        // Check if start time is after end time
        if (startTime > endTime)
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.StartTimeMustBeBeforeEndTime));

        // Check if the hour is before 08:00
        if (startTime.TimeOfDay < new TimeSpan(8, 0, 0))
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.CannotStartBefore08_00));

        // Check if the hour is after 23:59
        if (endTime.TimeOfDay > new TimeSpan(23, 59, 0))
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.EventCannotEndAfter23_59));

        //duration of the event is less than 1
        if ((endTime - startTime).TotalHours < 1)
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.EventDurationLessThan1Hour));

        //duration of the event is longer than 10
        if ((endTime - startTime).TotalHours > 10)
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.EventDurationGreaterThan10Hours));


        if (_EventStatus == EventStatus.Ready) _EventStatus = EventStatus.Draft;


        // Check if the start time is in the past
        if (DateTime.Now > startTime)
            return Result.Failure(Error.BadRequest(ErrorMessage.TimeRules.StartTimeCannotBeInPast));

        _StartDateTime = startDateTime;
        _EndDateTime = endDateTime;

        return Result.Success();
    }

    public Result MakeEventPublic()
    {
        if (_EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBePublic));

        if (_EventVisibility == EventVisibility.Public) return Result.Success();

        _EventVisibility = EventVisibility.Public;

        return Result.Success();
    }

    public Result MakeEventPrivate()
    {
        if (_EventStatus == EventStatus.Active)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.ActiveEventCannotBePrivate));

        if (_EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeModified));

        if (_EventVisibility == EventVisibility.Private) return Result.Success();

        if (_EventVisibility == EventVisibility.Public)
        {
            _EventVisibility = EventVisibility.Private;
            return Result.Success();
        }

        _EventStatus = EventStatus.Draft;

        return Result.Success();
    }

    public Result<ViaEvent> SetMaxNumberOfGuests(MaxNumberOfGuests maxGuests)
    {
        if (_EventStatus == EventStatus.Active && maxGuests.Value < _MaxNumberOfGuests.Value)
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.Capacity.ActiveEventCannotReduceMaxGuests));

        if (_EventStatus == EventStatus.Cancelled)
            return Result<ViaEvent>.Failure(
                Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeModified));

        _MaxNumberOfGuests = maxGuests;
        return Result<ViaEvent>.Success(this);
    }

    public Result<ViaEvent> MakeEventReady()
    {
        var dateTimeNow = DateTime.Now;
        var errors = new List<Error>();

        if (_EventStatus == EventStatus.Cancelled)
            errors.Add(Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeMadeReady));

        if (_EventTitle == null || string.IsNullOrWhiteSpace(_EventTitle.Value))
            errors.Add(Error.BadRequest(ErrorMessage.EventFields.TitleLengthOutOfRange));

        if (_EventTitle?.Value == "Working Title")
            errors.Add(Error.BadRequest(ErrorMessage.EventFields.TitleMustBeChangedFromDefault));

        if (_Description == null || string.IsNullOrWhiteSpace(_Description.Value))
            errors.Add(Error.BadRequest(ErrorMessage.EventFields.DescriptionLengthOutOfRange));

        if (_StartDateTime == null || _EndDateTime == null)
            errors.Add(Error.BadRequest(ErrorMessage.General.TimeIsNotValid));
        else if (_StartDateTime.Value.AddSeconds(30) < dateTimeNow)
            errors.Add(Error.BadRequest(ErrorMessage.EventLifecycle.EventInThePastCannotBeReady));
        else if (_StartDateTime.Value >= _EndDateTime.Value)
            errors.Add(Error.BadRequest(ErrorMessage.TimeRules.StartTimeMustBeBeforeEndTime));

        if (_MaxNumberOfGuests == null || _MaxNumberOfGuests.Value < 5 || _MaxNumberOfGuests.Value > 50)
            errors.Add(Error.BadRequest(ErrorMessage.Capacity.MaxGuestsOutOfRange));

        if (_EventVisibility == null) errors.Add(Error.BadRequest(ErrorMessage.EventFields.EventVisibilityIsNotSet));

        if (errors.Any()) return Result<ViaEvent>.Failure(errors);

        _EventStatus = EventStatus.Ready;
        return Result<ViaEvent>.Success(this);
    }

    public Result AddEventStartTime(StartDateTime startDateTime)
    {
        _StartDateTime = startDateTime;

        return Result.Success();
    }

    public Result EventEndTime(EndDateTime endDateTime)
    {
        _EndDateTime = endDateTime;

        return Result.Success();
    }


    public Result<ViaEvent> CancelEvent()
    {
        if (_EventStatus == EventStatus.Cancelled)
            return Result<ViaEvent>.Failure(
                Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeCancelled));

        _EventStatus = EventStatus.Cancelled;
        return Result<ViaEvent>.Success(this);
    }

    public Result<ViaEvent> ActivateEvent()
    {
        if (_EventStatus == EventStatus.Cancelled)
            return Result<ViaEvent>.Failure(
                Error.BadRequest(ErrorMessage.EventLifecycle.CancelledEventCannotBeActivated));
        var makeReadyResult = MakeEventReady();

        if (!makeReadyResult.IsSuccess) return Result<ViaEvent>.Failure(makeReadyResult.ErrorCollection);

        _EventStatus = EventStatus.Active;
        return Result<ViaEvent>.Success(this);
    }


    public Result AddGuestParticipation(GuestId guest)
    {
        var dateTimeNow = DateTime.Now;
        // Check if the event is public
        if (_EventVisibility != EventVisibility.Public)
            return Result.Failure(Error.BadRequest(ErrorMessage.Participation.OnlyPublicEventCanBeParticipated));

        if (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Ready ||
            _EventStatus == EventStatus.Cancelled)
            return Result.Failure(Error.BadRequest(ErrorMessage.Participation.OnlyActiveEventCanBeJoined));

        if (_StartDateTime.Value.AddSeconds(50) < dateTimeNow)
            return Result.Failure(Error.BadRequest(ErrorMessage.Participation.CannotParticipateInStartedEvent));

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
            return Result.Failure(Error.BadRequest(ErrorMessage.Capacity.EventIsFull));

        if (IsParticipant(guest))
            return Result.Failure(Error.BadRequest(ErrorMessage.Participation.GuestAlreadyParticipant));

        // Add the guest to the participants
        _GuestsParticipants.Add(guest);
        return Result.Success();
    }

    public bool IsParticipant(GuestId guestId)
    {
        return _GuestsParticipants.Contains(guestId);
    }

    public bool IsEventInPast()
    {
        var dateTimeNow = DateTime.Now;
        return _StartDateTime.Value.AddSeconds(50) < dateTimeNow;
    }

    public Result CancelGuestParticipation(GuestId guestId)
    {
        if (IsEventInPast())
            return Result.Failure(Error.BadRequest(ErrorMessage.Participation.CancelParticipationRejected));

        _GuestsParticipants.Remove(guestId);
        return Result.Success();

        // If not found, return error
        // return Result.Failure(Error.BadRequest(ErrorMessage.Participation.GuestNotParticipant));
    }

    public Result<Invitation> InviteGuest(GuestId guestId)
    {
        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Capacity.EventIsFull));

        var invitation = Invitation.Create(_eventId, guestId);

        if (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Cancelled)
            return Result<Invitation>.Failure(
                Error.BadRequest(ErrorMessage.Invitations.OnlyActiveOrReadyEventCanBeInvited));


        _Invitations.Add(invitation.Payload);
        return invitation;
    }


    public Result AcceptGuestInvitation(GuestId guestId)
    {
        var invitation =
            _Invitations.FirstOrDefault(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value);

        if (invitation == null)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Invitations.GuestNotInvitedToEvent));

        if (_EventStatus == EventStatus.Cancelled)
            return Result<Invitation>.Failure(
                Error.BadRequest(ErrorMessage.Participation.CancelledEventCannotBeJoined));

        if (!_Invitations.Any(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value))
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Invitations.GuestNotInvitedToEvent));

        if (_EventStatus != EventStatus.Active)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Participation.EventCannotYetBeJoined));

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Capacity.EventIsFull));

        if (invitation != null)
        {
            var acceptResult = invitation.Accept();

            if (acceptResult.IsSuccess)
            {
                _GuestsParticipants.Add(guestId);
                return Result.Success();
            }
        }

        return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));
    }

    public Result RejectGuestInvitation(GuestId guestId)
    {
        var invitation =
            _Invitations.FirstOrDefault(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value);

        if (invitation == null)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.Invitations.GuestNotInvitedToEvent));

        if (_EventStatus == EventStatus.Cancelled)
            return Result<Invitation>.Failure(
                Error.BadRequest(ErrorMessage.Participation.CancelledEventCannotBeDeclined));

        if (_EventStatus == EventStatus.Ready)
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventLifecycle.EventIsNotActive));

        var rejectResult = invitation.Decline();

        if (rejectResult.IsSuccess)
        {
            // If the guest is a participant, remove them from the participants list
            if (_GuestsParticipants.Contains(guestId)) _GuestsParticipants.Remove(guestId);

            return Result.Success();
        }

        return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));
    }
}