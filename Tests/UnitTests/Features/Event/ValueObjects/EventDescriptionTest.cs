using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.ValueObjects;

public class EventDescriptionTest
{
    [Fact]
    public void Create_ValidDescription_ReturnsSuccessResult()
    {
        // Arrange
        string validDescription = "A valid event description within the allowed range.";

        // Act
        var result = EventDescription.Create(validDescription);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validDescription, result.Payload.Value);
    }

    
     
    [Fact]
    public void Create_DescriptionLengthOutOfRange_ReturnsFailureResult()
    {
        // Arrange
        string invalidDescription = new string('x', 500); // Longer than allowed

        // Act
        var result = EventDescription.Create(invalidDescription);

        // Assert
        Assert.False(result.IsSuccess);
       
    }
    
}