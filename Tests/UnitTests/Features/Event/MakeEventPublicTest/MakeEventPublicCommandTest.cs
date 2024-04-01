using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.MakeEventPublic;

public class MakeEventPublicCommandTest
{
    
    [Fact]
    public void Create_ShouldReturnSuccess_WhenEventIdIsValid()
    {
        var validEventId = ViaEventTestFactory.ValidEventId().Value.ToString();
        // Act
        var result = MakeEventPublicCommand.Create(validEventId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEventId, result.Payload.EventId.Value.ToString());
        Assert.Equal(EventVisibility.Public, result.Payload.EventVisibility);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
    {
        // Arrange
        string invalidEventId = ""; // Assuming empty string is invalid

        // Act
        var result = MakeEventPublicCommand.Create(invalidEventId);

        // Assert
        Assert.False(result.IsSuccess);
    }
}