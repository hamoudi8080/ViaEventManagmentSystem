using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.AppEntry.Commands.Guest;

public class CreateGuestCommand : ICommand
{
    private CreateGuestCommand(GuestId guestId, FirstName firstName, LastName lastName, Email email)
    {
        GuestId = guestId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public GuestId GuestId { get; }
    public FirstName FirstName { get; }
    public LastName LastName { get; }
    public Email Email { get; }

    public static Result<CreateGuestCommand> Create(string guestId, string firstName, string lastName, string email)
    {
        var guestIdResult = GuestId.Create(guestId);
        var firstNameResult = FirstName.Create(firstName);
        var lastNameResult = LastName.Create(lastName);
        var emailResult = Email.Create(email);

        return Result
            .CombineResultsInto<CreateGuestCommand>(guestIdResult, firstNameResult, lastNameResult, emailResult)
            .WithPayloadIfSuccess(() => new CreateGuestCommand(guestIdResult.Payload!, firstNameResult.Payload!,
                lastNameResult.Payload!, emailResult.Payload!));
    }
}