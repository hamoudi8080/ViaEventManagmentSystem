 

using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

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
    
    
    [Fact]
    public void CreateMaxNumberOfGuests_OutOF_TheRange_AndReturnFalse()
    {
        //Arrange
        int numberOfGuests = 100;
        
        //Act
        var result = MaxNumberOfGuests.Create(numberOfGuests);
        
        //Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Contains("Maximum number of Guests cannot be less than 4 or more than 50 ", result.Error.Messages[0].ToString());
      
    }
}