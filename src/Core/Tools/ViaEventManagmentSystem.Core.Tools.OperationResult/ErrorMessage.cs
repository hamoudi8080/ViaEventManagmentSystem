namespace ViaEventManagmentSystem.Core.Tools.OperationResult;

public class ErrorMessage : Enumeration
{
   


    public static readonly ErrorMessage TitleMustBeSetBeforeMakingAnEventReady =
        new(0, "Title must be set before making an event ready");
    
    public static readonly ErrorMessage InvalidEmailAddress =
        new(0, "Invalid Email Address");
    public static readonly ErrorMessage InvalidInputError =
        new(0, "Invalid Input Error");
    
    public static readonly ErrorMessage DescriptionMustBeSetBeforeMakingAnEventReady =
        new(0, "Description must be set before making an event ready");

    public static readonly ErrorMessage EventDurationMustBeSetBeforeMakingAnEventReady =
        new(0, "Event duration must be set before making an event ready");

    public static readonly ErrorMessage EventInThePastCannotBeReady =
        new(0, "Event with start time at past cannot be made ready");
    
    public static readonly ErrorMessage FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace =
        new(0, "The first name must be between 2 and 25 characters or Is null or white space");
    public static readonly ErrorMessage LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace =
        new(0, "The first name must be between 2 and 25 characters or Is null or white space");
    public static readonly ErrorMessage EventSetToDraftBeforeMakingItReady =
        new(0, "Event must be in Draft Before Making It Ready");
    
    public static readonly ErrorMessage ActiveEventCannotBeMadeReady =
        new(0, "Active event cannot be made ready");

    public static readonly ErrorMessage CancelledEventCannotBemodified =
        new(0, "Cancelled event cannot be modified");
    
    public static readonly ErrorMessage CancelledEventCannotBePublic =
        new(0, "Cancelled event cannot be modified");

    public static readonly ErrorMessage CancelledEventCannotBeMadeReady =
        new(0, "Cancelled event cannot be made ready");

    public static readonly ErrorMessage CancelledEventCannotBeActivated =
        new(0, "Cancelled event cannot be activated");

    public static readonly ErrorMessage OnlyActiveEventsCanBeCancelled =
        new(0, "Only active events can be cancelled, if you intend to delete this event, please delete it instead");

    public static readonly ErrorMessage ActiveEventCannotBeDeleted =
        new(0, "Active event cannot be deleted, if you intend to cancel this event, please cancel it instead");

    public static readonly ErrorMessage TitleMustBeBetween3And75Chars =
        new(0, "Event title must be between 3 and 75 characters");

    public static readonly ErrorMessage DescriptionMustBeBetween0And250Chars =
        new(0, "Event Description must be between 0 and 250 characters");

    public static readonly ErrorMessage StartTimeMustBeBeforeEndTime =
        new(0, "Event start time cannot be after event end time");
    
    public static readonly  ErrorMessage StartTimeBefore1EndTimeAfter1 =
        new(0, "Event start time cannot be before 1:00 and endtime cannot be after 1:00");

    public static readonly ErrorMessage EventDurationGreaterThan10Hours =
        new(0, "Event Duration Cannot Be Greater Than 10 Hours");

    public static readonly ErrorMessage EventDurationLessThan1Hour =
        new(0, "Event Duration Cannot Be Less Than 1 Hours");

    public static readonly ErrorMessage EventCannotSpanBetween1AmAnd8Am =
        new(0, "Event cannot take place from 1am to 8am");
    
    
    public static readonly ErrorMessage EventcannotEndTimeAfter1AM =
        new(0, "Event cannot End After 1 AM");
    
    

    public static readonly ErrorMessage EventStartTimeCannotBeInPast =
        new(0, "Event start time must not be on past");

    public static readonly ErrorMessage EventCannotStartBefore8Am =
        new(0, "Event start time cannot be before 8 am");
    
    public static readonly ErrorMessage EventCannotEndTimeBefore8AM =
        new(0, "Event Cannot End Time Before 8 AM");

    public static readonly ErrorMessage EventStartAndEndDateMustBeSame =
        new(0, "Start And End Date Must Be Same");

    public static readonly ErrorMessage MaxGuestsNoMustBeWithin5and50 =
        new(0, "Maximum number of Guests cannot be less than 5 or more than 50 ");

    public static readonly ErrorMessage ActiveEventCanotBeModified =
        new(0, "Active event cannot be modified");

    public static readonly ErrorMessage ActiveEventCannotBePrivate =
        new(0, "Active event cannot be made private");

    public static readonly ErrorMessage ActiveEventCannotReduceMaxGuests =
        new(0, "Maximum number of guests cannot be reduced in an active event");

    public static readonly ErrorMessage UnParsableGuid =
        new(0, "The provided guid value is not parsable");
    public static readonly ErrorMessage EventAlreadyReady =
        new(0, "Event had already been ready");
    public static readonly ErrorMessage RequiredFieldsNotSet =
        new(0, "Required Fields Not Set to some value");
    
    public static readonly ErrorMessage EmailMustEndWithViaDK =
        new(0, "Email Must End With Via.DK");
     
    public static readonly ErrorMessage TextLengthOutOfRange =
        new(0, "Email Text Length Out Of Range");
    public static readonly ErrorMessage TextFormatInvalid =
        new(0, "Email Text Format Invalid");
    
    public static readonly ErrorMessage FirstNameCannotContainNumbers =
        
        new(0, "Firstname must not contain numbers");
    public static readonly ErrorMessage LastNameCannotContainNumbers =
        new(0, "Lastname must not contain numbers");
    
    public static readonly ErrorMessage FirstNameCannotContainSymbols =
        new(0, "FirstName Cannot Contain Symbols");
    public static readonly ErrorMessage LastNameCannotContainSymbols =
        new(0, "LastName Cannot Contain Symbols");
    
    public static readonly ErrorMessage FirstNameCannotBeNull =
        new(0, "FirstName Cannot be null, please provide a name");
    
    private ErrorMessage()
    {
    }

    private ErrorMessage(int value, string displayName) : base(value, displayName)
    {
    }
}