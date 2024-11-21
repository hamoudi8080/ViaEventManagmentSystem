using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Guest;

public class CreateGuestCommand : ICommand
{
    
    public Domain.Aggregates.Guests.Guest Guest { get; init; }
    private CreateGuestCommand(Domain.Aggregates.Guests.Guest guest)
    {

        Guest = guest;

    }

    public static Result<CreateGuestCommand> Create( string guestId, string firstName, string lastName, string email )
    {
        Result<GuestId> guestIdResult = GuestId.Create(guestId);
        Result<FirstName> firstNameResult = FirstName.Create(firstName);
        Result<LastName> lastNameResult = LastName.Create(lastName);
        Result<Email> emailResult = Email.Create(email);
         
/*
        if (guestIdResult.IsSuccess && firstNameResult.IsSuccess && lastNameResult.IsSuccess && emailResult.IsSuccess  )
        {
            Result<Domain.Aggregates.Guests.Guest> guest = Domain.Aggregates.Guests.Guest.Create(guestIdResult.Payload, 
                firstNameResult.Payload.Value, lastNameResult.Payload.Value, emailResult.Payload.Value);
            
            return Result<CreateGuestCommand>.Success( new CreateGuestCommand(guest.Payload));
        }
*/
        return Result<CreateGuestCommand>.Failure(Error.AddCustomError("Failed to create CreateGuestCommand due to invalid guestid, FirstName, LastName, Email or PhoneNumber"));
    }
    
}