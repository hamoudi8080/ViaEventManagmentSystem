using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests;

public class UpdateEmail
{
    [Fact]
    public void UpdateEmail_UpdatesGuestEmail()
    {
        
        // Arrange
        Name name = new Name("John","Jensen");
        
        Email oldEmail = new Email("old@example.com");
        
        Email newEmail = new Email("new@example.com");
        
       // Guest guest = new Guest(name, oldEmail);
        
        
        // Act
      //  guest.UpdateEmail(newEmail);

        // Assert
        //Assert.Equal(newEmail, guest.UpdateEmail(newEmail.Value.Remov);
    }
}