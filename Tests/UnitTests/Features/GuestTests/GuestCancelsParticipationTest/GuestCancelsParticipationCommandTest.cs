using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests.GuestCancelsParticipationTest;

public class GuestCancelsParticipationCommandTest
{
    
    [Fact]
    public void GivenEventIdAndGuestId_GuestCancelsParticipationCommandIsCreated()
    {
        //Arrange
        var guestId = GuestId.Create();
        var eventId = EventId.Create();
        
        //Act
        var result = GuestCancelsParticipationCommand.Create(eventId.Payload.Value.ToString(), guestId.Payload.Value.ToString());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
    }
}