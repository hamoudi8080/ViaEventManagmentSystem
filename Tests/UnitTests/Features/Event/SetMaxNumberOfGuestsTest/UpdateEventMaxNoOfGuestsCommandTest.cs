using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.SetMaxNumberOfGuestsTest;

public class UpdateEventMaxNoOfGuestsCommandTest
{
    [Fact]
     public void CreateUpdateEventMaxNoOfGuestsCommand_WithValidData_ShouldReturnSuccess()
     {
         // Arrange
         var id = ViaEventTestFactory.ValidEventId().Value.ToString();
         var maxNoOfGuests = 5;
         
         // Act
         var result = UpdateEventMaxNoOfGuestsCommand.Create(id, maxNoOfGuests);
         
         // Assert
         Assert.True(result.IsSuccess);
         Assert.NotNull(result.Payload);
        
     }
     
     [Fact]
     public void CreateUpdateEventMaxNoOfGuestsCommand_WithInvalidEventId_ShouldReturnFailure()
     {
         // Arrange
         var id = "invalidEventId";
         var maxNoOfGuests = 5;
         
         // Act
         var result = UpdateEventMaxNoOfGuestsCommand.Create(id, maxNoOfGuests);
         
         // Assert
         Assert.False(result.IsSuccess);
       
        
     }
}