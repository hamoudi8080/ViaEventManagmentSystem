
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public class ViaEvent : Aggregate<EventId>
{
    internal EventId _eventId => base.Id;
    internal EventTitle? _EventTitle { get; private set; }
    internal EventDescription? _Description { get; private set; }
    internal StartDateTime? _StartDateTime { get; private set; }
    internal EndDateTime? _EndDateTime { get; private set; }
    internal MaxNumberOfGuests? _MaxNumberOfGuests { get; private set; }
    internal EventVisibility? _EventVisibility { get; private set; }
    internal EventStatus _EventStatus { get; private set; }
    internal HashSet<GuestId> _GuestsParticipants { get; private set; }
    internal List<Invitation> _Invitations { get; private set; }
 

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

    public ViaEvent(EventId eventId, EventTitle? eventTitle, EventDescription? description,
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

    public Result UpdateTitle(EventTitle title)
    {
        if (_EventStatus == EventStatus.Active)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.ActiveEventCanotBeModified));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        if (_EventStatus == EventStatus.Ready)
        {
            _EventStatus = EventStatus.Draft;
        }

        _EventTitle = title;

        return Result.Success();
    }

    public Result UpdateDescription(EventDescription description)
    {
        if (_EventStatus == EventStatus.Ready)
        {
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

        _Description = description;


        return Result.Success();
    }

    public Result EventTimeDuration(StartDateTime? startDateTime, EndDateTime? endDateTime)
    {
        if (startDateTime == null || endDateTime == null)
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

        // Check if the hour is before 08:00
        if (startTime.TimeOfDay < new TimeSpan(8, 0, 0))
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventCannotStartBefore8Am));
        }

        // Check if the hour is after 23:59
        if (endTime.TimeOfDay > new TimeSpan(23, 59, 0))
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventCannotEndAfter1159Pm));
        }

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


        if (_EventStatus == EventStatus.Ready)
        {
            _EventStatus = EventStatus.Draft;
        }


        // Check if the start time is in the past
        if (DateTime.Now > startTime)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.EventStartTimeCannotBeInPast));
        }

        _StartDateTime = startDateTime;
        _EndDateTime = endDateTime;

        return Result.Success();
    }
    
    public Result MakeEventPublic()
    {
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBePublic));
        }

        if (_EventVisibility == EventVisibility.Public)
        {
            return Result.Success();
        }

        _EventVisibility = EventVisibility.Public;

        return Result.Success();
    }

    
    public Result MakeEventPrivate()
    {
        if (_EventStatus == EventStatus.Active)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.ActiveEventCannotBePrivate));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        if (_EventVisibility == EventVisibility.Private)
        {
            return Result.Success();
        }

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
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.ActiveEventCannotReduceMaxGuests));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }
        
        _MaxNumberOfGuests = maxGuests;
        return Result<ViaEvent>.Success(this);
    }
    
    public Result<ViaEvent> MakeEventReady()
    {
        DateTime dateTimeNow = DateTime.Now;
        var errors = new List<Error>();

        if (_EventStatus == EventStatus.Cancelled)
        {
            errors.Add(Error.BadRequest(ErrorMessage.CancelledEventCannotBeMadeReady));
        }

        if (_EventTitle == null || string.IsNullOrWhiteSpace(_EventTitle.Value))
        {
            errors.Add(Error.BadRequest(ErrorMessage.TitleMustBeBetween3And75Chars));
           
        }

        if (_EventTitle?.Value == "Working Title")
        {
            errors.Add(Error.BadRequest(ErrorMessage.TitleMustbeChangedFromDefault));
        }

        if (_Description == null || string.IsNullOrWhiteSpace(_Description.Value))
        {
            errors.Add(Error.BadRequest(ErrorMessage.DescriptionMustBeBetween0And250Chars));
            
        }

        if (_StartDateTime == null || _EndDateTime == null)
        {
            errors.Add(Error.BadRequest(ErrorMessage.TimeIsNotVaild));
        }
        else if (_StartDateTime.Value.AddSeconds(30) < dateTimeNow)
        {
            errors.Add(Error.BadRequest(ErrorMessage.EventInThePastCannotBeReady));
        }
        else if (_StartDateTime.Value >= _EndDateTime.Value)
        {
            errors.Add(Error.BadRequest(ErrorMessage.StartTimeMustBeBeforeEndTime));
        }

        if (_MaxNumberOfGuests == null || _MaxNumberOfGuests.Value < 5 || _MaxNumberOfGuests.Value > 50)
        {
            errors.Add(Error.BadRequest(ErrorMessage.MaxGuestsNoMustBeWithin5and50));
        }

        if (_EventVisibility == null)
        {
            errors.Add(Error.BadRequest(ErrorMessage.EventVisbilityIsNotSet));
        }

        if (errors.Any())
        {
            return Result<ViaEvent>.Failure(errors);
        }

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
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeCancelled));
        }

        _EventStatus = EventStatus.Cancelled;
        return Result<ViaEvent>.Success(this);
    }

    public Result<ViaEvent> ActivateEvent()
    {
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeActivated));
        }
        var makeReadyResult = MakeEventReady();

        if (!makeReadyResult.IsSuccess)
        {
            return Result<ViaEvent>.Failure(makeReadyResult.ErrorCollection);
        }
        
        _EventStatus = EventStatus.Active;
        return Result<ViaEvent>.Success(this);
    }
    


    public Result AddGuestParticipation(GuestId guest)
    {
        DateTime dateTimeNow = DateTime.Now;
        // Check if the event is public
        if (_EventVisibility != EventVisibility.Public)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.OnlyPublicEventCanBeParticipated));
        }

        if (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Ready ||
            _EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.OnlyActiveEventCanBeJoined));
        }

        if (_StartDateTime.Value.AddSeconds(50) < dateTimeNow)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CannotParticipatedInStartedEvent));
        }

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
        {
            return  Result.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }

        if (IsParticipant(guest))
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.GuestAlreadyParticipantAtEvent));
        }

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
        DateTime dateTimeNow = DateTime.Now;
        return _StartDateTime.Value.AddSeconds(50) < dateTimeNow;
    }

    public Result CancelGuestParticipation(GuestId guestId)
    {
        if (IsEventInPast())
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelParticipationRejected));
        }

      
        _GuestsParticipants.Remove(guestId);
            return Result.Success();
        

        return Result.Failure(Error.BadRequest(ErrorMessage.GuestNotParticipant));
    }

    public Result<Invitation> InviteGuest(GuestId guestId)
    {
        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }

        var invitation = Invitation.Create(_eventId, guestId);
        
        if (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Cancelled)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.OnlyActiveOrReadyEventCanBeInvited));
        }


        _Invitations.Add(invitation.Payload);
        return invitation;
    }
    

    public Result AcceptGuestInvitation(GuestId guestId)
    {
        var invitation = _Invitations.FirstOrDefault(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value);
        
        if (invitation == null)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.GuestIsNotInvitedToEvent));
        }
        
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeJoined));
        }

        if (!_Invitations.Any(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value))
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.GuestIsNotInvitedToEvent));
        }

        if (_EventStatus != EventStatus.Active)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventCannotYetBeJoined));
        }

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }

        if (invitation != null)
        {
            var acceptResult = invitation.Accept();

            if (acceptResult.IsSuccess)
            {
                _GuestsParticipants.Add(guestId);
                return Result.Success();
            }
        }

       

        return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
    }

    public Result RejectGuestInvitation(GuestId guestId)
    {
        var invitation =
            _Invitations.FirstOrDefault(i => i._GuestId.Value == guestId.Value && i._EventId.Value == _eventId.Value);
        
        if (invitation == null)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.GuestIsNotInvitedToEvent));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeDeclined));
        }
        
        if (_EventStatus == EventStatus.Ready)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventIsNotActive));
        }

        var rejectResult = invitation.Decline();


        if (rejectResult.IsSuccess)
        {
            // If the guest is a participant, remove them from the participants list
            if (_GuestsParticipants.Contains(guestId))
            {
                _GuestsParticipants.Remove(guestId);
            }
         
            return Result.Success();
        }

        return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
    }
}