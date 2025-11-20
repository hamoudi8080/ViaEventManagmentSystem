using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.GuestTests.GuestParticipationTest;

public class ParticipateGuestCommandTest
{
    [Fact]
    public void CreateParticipateGuestCommand_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var id = ViaEventTestFactory.ValidEventId().Value.ToString();
        var guestId = GuestFactory.ValidGuestId().Value.ToString();
        
        // Act
        var result = ParticipateGuestCommand.Create(id, guestId);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
       
    }
    
    [Fact]
    public void CreateParticipateGuestCommand_WithInvalidEventId_ShouldReturnFailure()
    {
        // Arrange
     
        var id = "invalidEventId";
        var guestId = GuestFactory.ValidGuestId().Value.ToString();
        
        // Act
        var result = ParticipateGuestCommand.Create(id, guestId);
        
        // Assert
        Assert.False(result.IsSuccess);
       
    }
}