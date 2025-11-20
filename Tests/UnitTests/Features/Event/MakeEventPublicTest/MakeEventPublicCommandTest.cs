using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

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
        Assert.NotNull(result.Payload);

        var cmd = result.Payload!;
        Assert.Equal(Guid.Parse(validEventId), cmd.EventId.Value);
        Assert.Equal(EventVisibility.Public, cmd.EventVisibility);

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