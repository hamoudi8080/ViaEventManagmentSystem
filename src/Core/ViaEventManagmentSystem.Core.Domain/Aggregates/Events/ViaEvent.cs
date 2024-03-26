using System.Text;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public class ViaEvent : Aggregate<EventId>
{
    internal static EventId _eventId { get; private set; }
    internal EventTitle? _EventTitle { get; private set; }
    internal EventDescription? _Description { get; private set; }
    internal StartDateTime? _StartDateTime { get; private set; }
    internal EndDateTime? _EndDateTime { get; private set; }
    internal MaxNumberOfGuests? _MaxNumberOfGuests { get; private set; }
    internal EventVisibility? _EventVisibility { get; private set; }
    internal EventStatus _EventStatus { get; private set; }
    internal List<GuestId> _GuestsParticipants { get; private set; }

    // internal Invitation _SendInvitations { get; private set; }

    

    internal List<Invitation> _Invitations { get; private set; }
    internal static List<InvitationRequest> _RequestInvitations { get; private set; }

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
        _GuestsParticipants = new List<GuestId>();
        _Invitations = new List<Invitation>();
        _RequestInvitations = new List<InvitationRequest>();
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

    public Result<ViaEvent> MakeEventReady()
    {
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeMadeReady));
        }

        StringBuilder errorMessage = new StringBuilder("The following fields are missing or have default values:");

        if (_EventTitle == null)
        {
            errorMessage.Append("\n- Title is missing");
        }
        else if (_EventTitle == EventTitle.Create("Working Title").Payload)
        {
            errorMessage.Append("\n- Title has default value");
        }

        if (_Description == null)
        {
            errorMessage.Append("\n- Description is missing");
        }
        else if (_Description == EventDescription.Create(string.Empty).Payload)
        {
            errorMessage.Append("\n- Description has default value");
        }

        if (_StartDateTime == null)
        {
            errorMessage.Append("\n- Start date and time are missing");
        }

        var fiveMinutesAgo = DateTime.Now.AddMinutes(-5);
        if (_StartDateTime.Value < fiveMinutesAgo)
        {
            errorMessage.Append("\n- Start date and time cannot be in the past");
        }
        else if (_StartDateTime == StartDateTime.Create(DateTime.Now).Payload)
        {
            errorMessage.Append("\n- Start date and time have default value");
        }


        if (_EndDateTime == null)
        {
            errorMessage.Append("\n- End date and time are missing");
        }

        if (_EventVisibility == null)
        {
            errorMessage.Append("\n- Visibility is missing");
        }

        if (_MaxNumberOfGuests == null)
        {
            errorMessage.Append("\n- Maximum number of guests is missing");
        }
        else if (_MaxNumberOfGuests.Value < 5 || _MaxNumberOfGuests.Value > 50)
        {
            errorMessage.Append("\n- Maximum number of guests must be between 5 and 50");
        }

        // Check if any error message is added
        if (errorMessage.Length > "The following fields are missing or have default values:".Length)
        {
            Console.WriteLine(errorMessage.ToString());
            return Result<ViaEvent>.Failure(Error.AddCustomError(errorMessage.ToString()));
        }

        // No errors found, make event ready
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

    public Result MakeEventPublic()
    {
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBePublic));
        }

        if (_EventVisibility == EventVisibility.Public)
        {
            // Event is already public, no need to change anything
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
            // Event is already private, no need to change anything
            return Result.Success();
        }

        _EventVisibility = EventVisibility.Private;
        _EventStatus = EventStatus.Draft;
/*
        // If the event is public and the status is Draft or Ready, change the status to Draft
        if (_EventVisibility == EventVisibility.Public &&
            (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Ready))
        {
            _EventVisibility = EventVisibility.Private;
            _EventStatus = EventStatus.Draft;
        }
*/
        return Result.Success();
    }


    public Result<ViaEvent> SetMaxNumberOfGuests(MaxNumberOfGuests maxGuests)
    {
        if (_EventStatus == EventStatus.Active &&
            maxGuests.Value <
            _MaxNumberOfGuests.Value) // Check if the new max number of guests is less than the current value
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.ActiveEventCannotReduceMaxGuests));
        }

        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified));
        }

        if (maxGuests.Value < 5 || maxGuests.Value > 50)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.MaxGuestsNoMustBeWithin5and50));
        }

        _MaxNumberOfGuests = maxGuests;

        //The this keyword is passed as an argument to the Success method, which means we're passing the current instance of the ViaEvent class.
        return Result<ViaEvent>.Success(this);
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

        // Make the event ready first
        var makeReadyResult = MakeEventReady();

        if (!makeReadyResult.IsSuccess)
        {
            return Result<ViaEvent>.Failure(makeReadyResult.Error);
        }

        // If making the event ready is successful, make it active
        _EventStatus = EventStatus.Active;
        return Result<ViaEvent>.Success(this);
    }


    // Method to get the current number of registered guests
    private Result<int> GetCurrentNumberOfGuests()
    {
        return _GuestsParticipants.Count;
    }
    // Method to register a guest for participation

    public Result AddIntendedGuestToEvent(GuestId guestId)
    {
        _GuestsParticipants.Add(guestId);
        return Result.Success();
    }


    public Result AddGuestParticipation(GuestId guest)
    {
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

        if (_StartDateTime.Value < DateTime.Now)
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CannotParticipatedInStartedEvent));
        }

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
        {
            Result.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }

        if (IsParticipant(guest))
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.GuestAlreadyParticipantAtEvent));
        }

        _GuestsParticipants.Add(guest);
        return Result.Success();
    }

    public bool IsParticipant(GuestId guestId)
    {
        return _GuestsParticipants.Contains(guestId);
    }

    public bool IsEventInPast()
    {
        return _StartDateTime.Value < DateTime.Now;
    }

    public Result CancelGuestParticipation(GuestId guestId)
    {
        if (IsEventInPast())
        {
            return Result.Failure(Error.BadRequest(ErrorMessage.CancelParticipationRejected));
        }

        _GuestsParticipants.Remove(guestId);
        return Result.Success();
    }


    public Result<Invitation> InviteGuest(GuestId guestId)
    {
        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value )
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }
        
        var invitation = Invitation.Create(_eventId, InvitationId.Create().Payload, guestId);
        if (_EventStatus == EventStatus.Draft || _EventStatus == EventStatus.Cancelled)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.OnlyActiveOrReadyEventCanBeInvited));
        }

       
        _Invitations.Add(invitation.Payload);
        return invitation;
    }

    public static Result<InvitationRequest> RequestInvitation(GuestId guestId)
    {
        var request = InvitationRequest.Create(RequestInvitationId.Create().Payload, _eventId, guestId);
        //new InvitationRequest( RequestInvitationId.Create().Payload, _eventId, guestId);
        _RequestInvitations.Add(request.Payload);
        return request;
    }
    
    
    public Result AcceptGuestInvitation(GuestId guestId)
    {
        var invitation = _Invitations.FirstOrDefault(i => i._GuestId == guestId && i._EventId == _eventId);
        if (_EventStatus == EventStatus.Cancelled)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.CancelledEventCannotBeJoined));
        }
        if (invitation == null)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.GuestNotInvited));
        }


     
        if (_EventStatus != EventStatus.Active)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
        }

        if (_GuestsParticipants.Count >= _MaxNumberOfGuests.Value)
        {
            return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.EventIsFull));
        }

        var acceptResult = invitation.Accept();
        
        if (acceptResult.IsSuccess)
        {
            _GuestsParticipants.Add(guestId);
            return Result.Success();
        }

        return Result<Invitation>.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
    }

    
}