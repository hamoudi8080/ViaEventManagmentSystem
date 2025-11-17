using ViaEventManagementSystem.Core.AppEntry.Commands.Guest;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests;

public class CreateGuestCommandTest
{
    [Fact]
    public void CreateGuestCommand_WhenValid_ShouldCreateGuestSuccess()
    {
        // Arrange
        var id = GuestId.Create();
        var email = "John@via.dk";
        var firstName = "john";
        var lastname = "resho";


        // Act
        var createGuest = CreateGuestCommand.Create(id.Payload.Value.ToString(), firstName, lastname, email);

        //Assert
        Assert.True(createGuest.IsSuccess);
    }
}