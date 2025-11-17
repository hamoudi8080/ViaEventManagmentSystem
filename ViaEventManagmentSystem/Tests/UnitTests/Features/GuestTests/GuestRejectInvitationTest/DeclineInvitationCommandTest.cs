using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.GuestTests.GuestRejectInvitationTest;

public class DeclineInvitationCommandTest
{
    [Fact]
    public void Create_WhenInvoked_ShouldReturnDeclineInvitationCommand()
    {
        // Arrange
        var id = ViaEventTestFactory.ValidEventId().Value.ToString();
        var guestId = GuestFactory.ValidGuestId().Value.ToString();


        // Act
        var result = DeclineInvitationCommand.Create(id, guestId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
    }
}