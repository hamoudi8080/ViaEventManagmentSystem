// NOTE: your namespace has a typo ("Managment"). Consider renaming to "Management".

namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

// Assuming Enumeration(int value, string displayName)
public sealed class ErrorMessage : Enumeration
{
    // Add legacy/compatibility aliases for test code:
    public static readonly ErrorMessage ActiveEventCannotReduceMaxGuests = Capacity.ActiveEventCannotReduceMaxGuests;
    public static readonly ErrorMessage CancelledEventCannotBemodified = EventLifecycle.CancelledEventCannotBeModified;
    public static readonly ErrorMessage MaxGuestsNoMustBeWithin5and50 = Capacity.MaxGuestsOutOfRange;
    public static readonly ErrorMessage GuestIsNotInvitedToEvent = Invitations.GuestNotInvitedToEvent;
    public static readonly ErrorMessage StartTimeMustBeBeforeEndTime = TimeRules.StartTimeMustBeBeforeEndTime;
    public static readonly ErrorMessage EventDurationLessThan1Hour = TimeRules.EventDurationLessThan1Hour;
    public static readonly ErrorMessage EventDurationGreaterThan10Hours = TimeRules.EventDurationGreaterThan10Hours;
    public static readonly ErrorMessage EventCannotStartBefore8Am = TimeRules.CannotStartBefore08_00;
    public static readonly ErrorMessage EventStartTimeCannotBeInPast = TimeRules.StartTimeCannotBeInPast;
    public static readonly ErrorMessage CancelledEventCannotBePublic = EventLifecycle.CancelledEventCannotBePublic;
    public static readonly ErrorMessage ActiveEventCannotBePrivate = EventLifecycle.ActiveEventCannotBePrivate;

    public static readonly ErrorMessage
        CancelledEventCannotBeActivated = EventLifecycle.CancelledEventCannotBeActivated;

    public static readonly ErrorMessage
        CancelledEventCannotBeMadeReady = EventLifecycle.CancelledEventCannotBeMadeReady;

    public static readonly ErrorMessage EventInThePastCannotBeReady = EventLifecycle.EventInThePastCannotBeReady;
    public static readonly ErrorMessage TitleMustbeChangedFromDefault = EventFields.TitleMustBeChangedFromDefault;
    public static readonly ErrorMessage ActiveEventCanotBeModified = EventLifecycle.ActiveEventCannotBeModified;
    public static readonly ErrorMessage DescriptionMustBeBetween0And250Chars = EventFields.DescriptionLengthOutOfRange;
    public static readonly ErrorMessage TitleMustBeBetween3And75Chars = EventFields.TitleLengthOutOfRange;

    public static readonly ErrorMessage EventIsFull = Capacity.EventIsFull;
    public static readonly ErrorMessage GuestAlreadyParticipantAtEvent = Participation.GuestAlreadyParticipant;
    public static readonly ErrorMessage CancelParticipationRejected = Participation.CancelParticipationRejected;
    public static readonly ErrorMessage OnlyActiveEventCanBeJoined = Participation.OnlyActiveEventCanBeJoined;

    public static readonly ErrorMessage OnlyPublicEventCanBeParticipated =
        Participation.OnlyPublicEventCanBeParticipated;

    public static readonly ErrorMessage
        CannotParticipatedInStartedEvent = Participation.CannotParticipateInStartedEvent;

    public static readonly ErrorMessage CancelledEventCannotBeJoined = Participation.CancelledEventCannotBeJoined;
    public static readonly ErrorMessage EventCannotYetBeJoined = Participation.EventCannotYetBeJoined;

    public static readonly ErrorMessage CancelledEventCannotBeDeclined = Participation.CancelledEventCannotBeDeclined;
    public static readonly ErrorMessage EventIsNotActive = EventLifecycle.EventIsNotActive;


    public static readonly ErrorMessage EmailMustEndWithViaDK = Email.EmailMustEndWithViaDK;
    public static readonly ErrorMessage TextFormatInvalid = Email.TextFormatInvalid;

    public static readonly ErrorMessage FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace =
        PersonName.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace;

    public static readonly ErrorMessage LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace =
        PersonName.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace;

    public static readonly ErrorMessage FirstNameCannotContainNumbers = PersonName.FirstNameCannotContainNumbers;
    public static readonly ErrorMessage LastNameCannotContainSymbols = PersonName.LastNameCannotContainSymbols;

    private ErrorMessage()
    {
    }

    private ErrorMessage(int value, string displayName) : base(value, displayName)
    {
    }

    // ─────────────────────────── General / Infrastructure (1000+) ───────────────────────────
    public static class General
    {
        public static readonly ErrorMessage InvalidInput = new(1000, "Invalid input.");
        public static readonly ErrorMessage UnparsableGuid = new(1001, "The provided GUID value is not parsable.");

        public static readonly ErrorMessage EventCreationFailed =
            new(1002, "Event creation failed when adding to repository.");

        public static readonly ErrorMessage EventNotFound = new(1003, "Event not found.");
        public static readonly ErrorMessage TimeIsNotValid = new(1004, "Please provide a valid time.");
    }

    // ─────────────────────────── Event lifecycle / status (2000+) ───────────────────────────
    public static class EventLifecycle
    {
        public static readonly ErrorMessage EventMustBeDraft = new(2000, "Event status must be Draft.");

        public static readonly ErrorMessage EventMustBeDraftBeforeReady =
            new(2001, "Event must be Draft before it can be made Ready.");

        public static readonly ErrorMessage EventAlreadyReady = new(2002, "Event is already Ready.");

        public static readonly ErrorMessage ActiveEventCannotBeMadeReady =
            new(2003, "Active event cannot be made Ready.");

        public static readonly ErrorMessage CancelledEventCannotBeMadeReady =
            new(2004, "Cancelled event cannot be made Ready.");

        public static readonly ErrorMessage CancelledEventCannotBeActivated =
            new(2005, "Cancelled event cannot be activated.");

        public static readonly ErrorMessage CancelledEventCannotBeCancelled =
            new(2006, "Cancelled event cannot be cancelled.");

        public static readonly ErrorMessage CancelledEventCannotBeModified =
            new(2007, "Cancelled event cannot be modified.");

        public static readonly ErrorMessage CancelledEventCannotBePublic =
            new(2008, "Cancelled event cannot be made Public.");

        public static readonly ErrorMessage ActiveEventCannotBePrivate =
            new(2011, "Active event cannot be made Private.");

        public static readonly ErrorMessage ActiveEventCannotBeModified = new(2012, "Active event cannot be modified.");
        public static readonly ErrorMessage EventIsNotActive = new(2013, "Event is not Active.");

        public static readonly ErrorMessage EventInThePastCannotBeReady =
            new(2014, "An event with start time in the past cannot be made Ready.");

        public static readonly ErrorMessage EventMustBeActive = new(2015, "Event must be Active.");
    }

    // ─────────────────────────── Required fields / content (3000+) ───────────────────────────
    public static class EventFields
    {
        public static readonly ErrorMessage RequiredFieldsNotSet = new(3000, "Required fields are missing.");
        public static readonly ErrorMessage EventVisibilityIsNotSet = new(3001, "Event visibility is missing.");

        public static readonly ErrorMessage TitleMustBeChangedFromDefault =
            new(3003, "Event title must be changed from the default.");

        public static readonly ErrorMessage TitleLengthOutOfRange =
            new(3002, "Event title must be between 3 and 75 characters.");

        public static readonly ErrorMessage DescriptionLengthOutOfRange =
            new(3005, "Event description must be between 0 and 250 characters.");

        public static readonly ErrorMessage DescriptionMustBeSetBeforeReady =
            new(3006, "Description must be set before making an event Ready.");

        public static readonly ErrorMessage EventDurationMustBeSetBeforeReady =
            new(3007, "Event duration must be set before making an event Ready.");
    }

    // ─────────────────────────── Time / date rules (4000+) ───────────────────────────
    public static class TimeRules
    {
        public static readonly ErrorMessage StartTimeMustBeBetween08_00And23_59 =
            new(4000, "Start time must be between 08:00 and 23:59.");

        public static readonly ErrorMessage StartTimeMustBeBeforeEndTime =
            new(4001, "Event start time must be before event end time.");

        public static readonly ErrorMessage StartTimeCannotBeInPast =
            new(4002, "Event start time must not be in the past.");

        public static readonly ErrorMessage StartAndEndDateMustBeSame =
            new(4003, "Start and end date must be the same.");

        public static readonly ErrorMessage CannotStartBefore08_00 = new(4004, "Event cannot start before 08:00.");
        public static readonly ErrorMessage CannotEndBefore08_00 = new(4005, "Event cannot end before 08:00.");
        public static readonly ErrorMessage EventCannotEndAfter23_59 = new(4006, "Event cannot end after 23:59.");

        public static readonly ErrorMessage EventCannotSpanBetween01_00And08_00 =
            new(4007, "Event cannot take place between 01:00 and 08:00.");

        public static readonly ErrorMessage EventDurationGreaterThan10Hours =
            new(4008, "Event duration cannot be greater than 10 hours.");

        public static readonly ErrorMessage EventDurationLessThan1Hour =
            new(4009, "Event duration cannot be less than 1 hour.");
    }

    // ─────────────────────────── Capacity rules (5000+) ───────────────────────────
    public static class Capacity
    {
        public static readonly ErrorMessage MaxGuestsOutOfRange =
            new(5000, "Maximum number of Guests cannot be less than 5 or more than 50 ");

        public static readonly ErrorMessage ActiveEventCannotReduceMaxGuests =
            new(5001, "Maximum number of guests cannot be reduced for an Active event.");

        public static readonly ErrorMessage EventIsFull =
            new(5002, "You cannot invite guests because the event is full.");
    }

    // ─────────────────────────── Invitations (6000+) ───────────────────────────
    public static class Invitations
    {
        public static readonly ErrorMessage OnlyActiveOrReadyEventCanBeInvited =
            new(6000, "Only Active or Ready events can send invitations.");

        public static readonly ErrorMessage
            GuestNotInvitedToEvent = new(6001, "The guest is not invited to the event.");
    }

    // ─────────────────────────── Participation / joining (7000+) ───────────────────────────
    public static class Participation
    {
        public static readonly ErrorMessage OnlyPublicEventCanBeParticipated =
            new(7000, "Only Public events can be participated in.");

        public static readonly ErrorMessage OnlyActiveEventCanBeJoined =
            new(7001, "Participants can only join an Active event.");

        public static readonly ErrorMessage EventCannotYetBeJoined = new(7002, "The event cannot yet be joined.");

        public static readonly ErrorMessage CancelledEventCannotBeJoined =
            new(7003, "Cancelled events cannot be joined.");

        public static readonly ErrorMessage EventIsPrivate =
            new(7004, "The event is Private and cannot be joined without a valid reason.");

        public static readonly ErrorMessage CannotParticipateInStartedEvent =
            new(7005, "Only future events can be participated in.");

        public static readonly ErrorMessage CancelParticipationRejected =
            new(7006, "You cannot cancel your participation for past or ongoing events.");

        public static readonly ErrorMessage GuestAlreadyParticipant =
            new(7007, "Guest is already a participant of this event.");

        public static readonly ErrorMessage
            GuestNotParticipant = new(7008, "Guest is not a participant of this event.");

        public static readonly ErrorMessage CancelledEventCannotBeDeclined =
            new(7009, "A cancelled event cannot be declined.");
    }

    // ─────────────────────────── Email validation (8000+) ───────────────────────────
    public static class Email
    {
        public static readonly ErrorMessage InvalidEmailAddress = new(8000, "Invalid email address.");
        public static readonly ErrorMessage EmailMustEndWithViaDK = new(8001, "Email must end with @via.dk.");
        public static readonly ErrorMessage TextLengthOutOfRange = new(8002, "Email text length is out of range.");
        public static readonly ErrorMessage TextFormatInvalid = new(8003, "Email text format is invalid.");
    }

    // ─────────────────────────── Person name validation (9000+) ───────────────────────────
    public static class PersonName
    {
        public static readonly ErrorMessage FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace = new(9000,
            "First name must be between 2 and 25 characters and not empty or whitespace.");

        public static readonly ErrorMessage LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace = new(9001,
            "Last name must be between 2 and 25 characters and not empty or whitespace.");

        public static readonly ErrorMessage FirstNameCannotContainNumbers =
            new(9002, "First name must not contain numbers.");

        public static readonly ErrorMessage LastNameCannotContainNumbers =
            new(9003, "Last name must not contain numbers.");

        public static readonly ErrorMessage FirstNameCannotContainSymbols =
            new(9004, "First name must not contain symbols.");

        public static readonly ErrorMessage LastNameCannotContainSymbols =
            new(9005, "Last name must not contain symbols.");

        public static readonly ErrorMessage FirstNameCannotBeNull =
            new(9006, "First name cannot be null; please provide a name.");

        public static readonly ErrorMessage FirstNameLengthInvalid = new(9007, "First name length is invalid.");
        public static readonly ErrorMessage LastNameLengthInvalid = new(9008, "Last name length is invalid.");
    }
}