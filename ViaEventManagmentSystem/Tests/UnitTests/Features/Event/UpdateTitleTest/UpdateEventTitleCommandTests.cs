using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.UpdateTitleTest;

public class UpdateEventTitleCommandTests
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenEventIdAndEventTitleAreValid()
    {
        // Arrange
        var validEventTitle = "FootBallHall";

        // Act
        var result =
            UpdateEventTitleCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventTitle);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEventTitle, result.Payload.EventTitle.Value);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
    {
        // Arrange
        var invalidEventId = "";
        var validEventTitle = "validEventTitle";

        // Act
        var result = UpdateEventTitleCommand.Create(invalidEventId, validEventTitle);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventTitleIsInvalid()
    {
        // Arrange
        var validEventId = "validEventId";
        var invalidEventTitle = ""; // Assuming empty string is invalid

        // Act
        var result = UpdateEventTitleCommand.Create(validEventId, invalidEventTitle);

        // Assert
        Assert.False(result.IsSuccess);
    }
}