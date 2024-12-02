using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.ReadiesEventTest;

public class MakeEventReadyCommandTest
{
    [Fact]
    public void GivenDraftEvent_WithValidData_WhenMakeEventReady_ThenEventIsMadeReady()
    {
        // Arrange
        var viaEvent = ViaEventTestFactory.DraftEvent();
        // Act
        var result = MakeEventReadyCommand.Create(viaEvent._eventId.Value.ToString());
        // Assert
        Assert.True(result.IsSuccess);
      
    }
}