using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;


namespace UnitTests.Features.GuestTests.GuestRejectInvitationTest;

public abstract class GuestRejectInvitationTest
{
    public class S1
    {
        [Fact]
        public void S1_GivenAnActiveEvent_RegisteredGuest_InvitaionRegistered_asRejected()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.ValidGuestId();
            activeEvent.InviteGuest(guest);

            // Act
            var result = activeEvent.RejectGuestInvitation(guest);

            // Assert
            Assert.True(result.IsSuccess);
        
        }
    }

    public class S2
    {
        [Fact]
        public void S2_GivenAnActiveEvent_RegisteredGuest_AcceptedInvitation_InvitationRegistered_asRejected()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.ValidGuestId();
            activeEvent.InviteGuest(guest);
            activeEvent.AcceptGuestInvitation(guest);

            // Act
            var result = activeEvent.RejectGuestInvitation(guest);

            // Assert
            Assert.True(result.IsSuccess);
      
        }
    }

    public class F1
    {
        [Fact]
        public void F1_GivenAnExistingEvent_RegisteredGuest_NoInvitation_GuestAcceptsInvitation_RequestIsRejected()
        {
            // Arrange
            var existingEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.ValidGuestId();

            // Act
            var result = existingEvent.AcceptGuestInvitation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.GuestIsNotInvitedToEvent.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }
    
    
    public class F2
    {
        [Fact]
        public void F2_GivenACancelledEvent_RegisteredGuest_PendingInvitation_GuestDeclinesInvitation_RequestIsRejected()
        {
            // Arrange
            var cancelledEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.ValidGuestId();
       
            cancelledEvent.InviteGuest(guest);
            cancelledEvent.CancelEvent();
            // Act
            var result = cancelledEvent.RejectGuestInvitation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.CancelledEventCannotBeDeclined.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }
    
    public class F3
    {
        [Fact]
        public void F3_GivenAReadyEvent_RegisteredGuest_PendingInvitation_GuestAcceptsInvitation_RequestIsRejected()
        {
            // Arrange
            var readyEvent = ViaEventTestFactory.ReadyEvent();
            var guest = GuestFactory.ValidGuestId();
            readyEvent.InviteGuest(guest);
            readyEvent.AcceptGuestInvitation(guest);

            // Act
            var result = readyEvent.RejectGuestInvitation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventIsNotActive.ToString(), result.Error.Messages[0].ToString());
        }
    }
}