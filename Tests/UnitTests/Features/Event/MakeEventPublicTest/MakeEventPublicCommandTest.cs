using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.MakeEventPublicTest;

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

    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
    {
        // Arrange
        string invalidEventId = "";

        // Act
        var result = MakeEventPublicCommand.Create(invalidEventId);

        // Assert
        Assert.False(result.IsSuccess);
    }
}