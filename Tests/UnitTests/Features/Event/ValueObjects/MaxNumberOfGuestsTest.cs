 

using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.ValueObjects;

public class MaxNumberOfGuestsTest
{
    [Fact]
    public void CreateMaxNumberOfGuests_Within_TheRange_AndReturnSuccess()
    {
        //Arrange
        int numberOfGuests = 30;
        
        //Act
        var result = MaxNumberOfGuests.Create(numberOfGuests);
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(numberOfGuests, result.Payload.Value);
      
    }
    
 
}