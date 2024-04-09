using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests.InviteGuestTest;

public class InviteGuestCommandTest
{
    [Fact]
    public void GivenValidEventIdAndGuestId_InviteGuestCreated()
    {
        // Arrange
        var eventId = ViaEventTestFactory.ValidEventId();
        var guestId = GuestFactory.ValidGuestId();
       

        // Act
        var inviteGuestCommand = InviteGuestCommand.Create(eventId.Value.ToString(), guestId.Value.ToString());

        // Assert
        Assert.True(inviteGuestCommand.IsSuccess);
        Assert.NotNull(inviteGuestCommand.Payload);
    }
}