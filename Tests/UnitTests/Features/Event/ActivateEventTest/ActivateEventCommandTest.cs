using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.ActivateEventTest;

public class ActivateEventCommandTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenEventIdIsValid()
    {
        var validEventId = EventId.Create();
        // Act
        var result = ActivateEventCommand.Create(validEventId.Payload.Value.ToString());

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEventId.Payload.Value.ToString(), result.Payload.EventId.Value.ToString());

    }
    
}