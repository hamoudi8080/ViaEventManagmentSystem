using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests;

public class UpdateEmail
{
    /*
    [Fact]
    public void UpdateEmail_WithValidEmail_ShouldUpdateEmail()
    {
        // Arrange
        GuestId guestId = new GuestId(Guid.NewGuid());
        Name initialName = new Name("John", "Doe");
        Email initialEmail = new Email("john.doe@example.com");
        Guest guest = new Guest(initialName, guestId, initialEmail);
        Email newEmail = new Email("johndoe@example.com");

        // Act
        guest.UpdateEmail(newEmail);

        // Assert
    
    }
    
    [Fact]
    public void UpdateEmail_WithNullEmail_ShouldThrowArgumentNullException()
    {
        // Arrange
        var guestId = new GuestId(Guid.NewGuid());
        var initialName = new Name("John", "Doe");
        var initialEmail = new Email("john.doe@example.com");
        var guest = new Guest(initialName, guestId, initialEmail);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => guest.UpdateEmail(null));
    }

    [Fact]
    public void UpdateEmail_WithInvalidEmail_ShouldThrowArgumentException()
    {
        // Arrange
        var guestId = new GuestId(Guid.NewGuid());
        var initialName = new Name("John", "Doe");
        var initialEmail = new Email("john.doe@example.com");
        var guest = new Guest(initialName, guestId, initialEmail);
        var invalidEmail = new Email("invalidemail"); // invalid email format

        // Act & Assert
        Assert.Throws<ArgumentException>(() => guest.UpdateEmail(invalidEmail));
    }
    */
}