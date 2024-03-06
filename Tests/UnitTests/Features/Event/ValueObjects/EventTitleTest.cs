using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.ValueObjects;

public class EventTitleTest
{
    [Fact]
    public void Create_ValidTitle_ReturnsSuccessResult()
    {
        // Arrange
        string validTitle = "Valid Title";

        // Act
        var result = EventTitle.Create(validTitle);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validTitle, result.Payload.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_NullOrEmptyTitle_ReturnsFailureResult(string title)
    {
        
        // Act
        var result = EventTitle.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        
        Assert.NotNull(result.Error);


        Assert.Contains("Invalid Input Error", result.Error.Messages[0].ToString());
        
    }

 
    [Fact]
    public void Create_InvalidTitle_ReturnsFailureResult()
    {
        // Arrange
        string invalidTitle = "12"; // Invalid title

        // Act
        var result = EventTitle.Create(invalidTitle);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Contains("Invalid Input Error", result.Error.Messages[0].ToString());
    }
   
}