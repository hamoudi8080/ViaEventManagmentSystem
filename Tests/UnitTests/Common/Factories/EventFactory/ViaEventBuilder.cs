using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Common.Factories.EventFactory;

public class ViaEventBuilder
{ 
    private EventId _eventId;
    private EventTitle? _eventTitle;
    private EventDescription? _description;
    private StartDateTime? _startDateTime;
    private EndDateTime? _endDateTime;
    private MaxNumberOfGuests? _maxNumberOfGuests;
    private EventVisibility? _eventVisibility;
    private EventStatus _eventStatus = EventStatus.Draft;
    private HashSet<GuestId> _guestsParticipants = new();
    private List<Invitation> _invitations = new();

    private ViaEventBuilder(EventId eventId)
    {
        _eventId = eventId;
    }

    public static ViaEventBuilder Create(EventId eventId)
    {
        return new ViaEventBuilder(eventId);
    }

    public ViaEventBuilder WithTitle(EventTitle title)
    {
        _eventTitle = title;
        return this;
    }

    public ViaEventBuilder WithDescription(EventDescription description)
    {
        _description = description;
        return this;
    }

    public ViaEventBuilder WithStartDateTime(StartDateTime startDateTime)
    {
        _startDateTime = startDateTime;
        return this;
    }

    public ViaEventBuilder WithEndDateTime(EndDateTime endDateTime)
    {
        _endDateTime = endDateTime;
        return this;
    }

    public ViaEventBuilder WithMaxNumberOfGuests(MaxNumberOfGuests maxGuests)
    {
        _maxNumberOfGuests = maxGuests;
        return this;
    }

    public ViaEventBuilder WithVisibility(EventVisibility visibility)
    {
        _eventVisibility = visibility;
        return this;
    }

    public ViaEventBuilder WithStatus(EventStatus status)
    {
        _eventStatus = status;
        return this;
    }

    public ViaEventBuilder AddParticipant(GuestId guest)
    {
        _guestsParticipants.Add(guest);
        return this;
    }

    public ViaEventBuilder AddInvitation(Invitation invitation)
    {
        _invitations.Add(invitation);
        return this;
    }

    public Result<ViaEvent> Build()
    {
        // Validate all required fields before building
        if (_eventId == null)
        {
            return Result<ViaEvent>.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));
        }

        if (_startDateTime == null)
        {
            _startDateTime = StartDateTime.Create(DateTime.Now).Payload;
        }

        if (_eventTitle == null)
        {
            _eventTitle = EventTitle.Create("Default Title").Payload;
        }

        if (_description == null)
        {
            _description = EventDescription.Create(string.Empty).Payload;
        }

        if (_maxNumberOfGuests == null)
        {
            _maxNumberOfGuests = MaxNumberOfGuests.Create(5).Payload;
        }

        if (_eventVisibility == null)
        {
            _eventVisibility = EventVisibility.Private;
        }

        var viaEvent = new ViaEvent(
            _eventId,
            _eventTitle,
            _description,
            _startDateTime,
            _endDateTime,
            _maxNumberOfGuests,
            _eventVisibility,
            _eventStatus
        );

        foreach (var guest in _guestsParticipants)
        {
            viaEvent.AddGuestParticipation(guest);
        }

        foreach (var invitation in _invitations)
        {
            viaEvent._Invitations.Add(invitation); // Add invitations directly
        }

        return Result<ViaEvent>.Success(viaEvent);
    }
}