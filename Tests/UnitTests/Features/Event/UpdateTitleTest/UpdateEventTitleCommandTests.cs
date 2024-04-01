using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.UpdateTitleTest;

public class UpdateEventTitleCommandTests
{
    
    [Fact]
    public void Create_ShouldReturnSuccess_WhenEventIdAndEventTitleAreValid()
    {
        // Arrange
        string validEventTitle = "FootBallHall";

        // Act
        var result = UpdateEventTitleCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventTitle);

        // Assert
        Assert.True(result.IsSuccess);
        //Assert.Equal(validEventId, result.Payload.EventId.Value
        Assert.Equal(validEventTitle, result.Payload.EventTitle.Value);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
    {
        // Arrange
        string invalidEventId = ""; // Assuming empty string is invalid
        string validEventTitle = "validEventTitle";

        // Act
        var result = UpdateEventTitleCommand.Create(invalidEventId, validEventTitle);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventTitleIsInvalid()
    {
        // Arrange
        string validEventId = "validEventId";
        string invalidEventTitle = ""; // Assuming empty string is invalid

        // Act
        var result = UpdateEventTitleCommand.Create(validEventId, invalidEventTitle);

        // Assert
        Assert.False(result.IsSuccess);
    }
}