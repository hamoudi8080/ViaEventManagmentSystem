using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests.GuestAcceptInvitationTest;

public class AcceptInvitationCommandTest
{
    [Fact]
    public void AcceptInvitationCommand_ValidData_ReturnsAcceptInvitationCommand()
    {
        // arrange 
        var guestId = GuestId.Create();
        var eventId = EventId.Create();
        
        // act
        var result = AcceptInvitationCommand.Create(eventId.Payload.Value.ToString(), guestId.Payload.Value.ToString());
        
        // assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
        

    }

}