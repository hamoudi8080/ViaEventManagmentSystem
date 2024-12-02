using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Core.AppEntry.Commands.Guest;

public class CreateGuestCommand : ICommand
{
    public ViaGuest Guest { get; }

    private CreateGuestCommand(ViaGuest guest)
    {
        Guest = guest;
    }

    public static Result<CreateGuestCommand> Create(string guestId, string firstName, string lastName, string email)
    {
        Result<GuestId> guestIdResult = GuestId.Create(guestId);
        Result<FirstName> firstNameResult = FirstName.Create(firstName);
        Result<LastName> lastNameResult = LastName.Create(lastName);
        Result<Email> emailResult = Email.Create(email);

        var result =
            Result.CombineFromOthers<CreateGuestCommand>(guestIdResult, firstNameResult, lastNameResult, emailResult);

        return Result<CreateGuestCommand>.WithPayloadIfSuccess(result,
            () => new CreateGuestCommand(new ViaGuest(guestIdResult.Payload!, firstNameResult.Payload!,
                lastNameResult.Payload!, emailResult.Payload!)));
    }
}