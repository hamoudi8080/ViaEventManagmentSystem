using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

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

    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_NullOrWhiteSpaceDescription_ReturnsFailureResult(string description)
    {
        // Act
        var result = EventDescription.Create(description);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("The Description is null or white spaces", result.ErrorMessage);
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
        Assert.Contains("The Description Must be between 1 and 400 characters", result.ErrorMessage);
    }
    
}