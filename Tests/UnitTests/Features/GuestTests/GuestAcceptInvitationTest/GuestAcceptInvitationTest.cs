using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestAcceptInvitationTest;

public abstract class GuestAcceptInvitationTest
{
    public class S1
    {
        [Fact]
        public void GivenAnActiveEvent_RegisteredGuest_pendingInvitation()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.CreateGuest();
            var invitation = activeEvent.InviteGuest(guest._Id);


            // Act
            var result = activeEvent.AcceptGuestInvitation(guest._Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(InvitationStatus.Accepted, invitation.Payload._InvitationStatus);
        }
    }

    public class F1
    {
        [Fact]
        public void GivenAnExistingEvent_NoInvitationForGuest()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.CreateGuest();

            // Act
            var result = activeEvent.AcceptGuestInvitation(guest._Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.GuestNotInvited.ToString(), result.Error.Messages[0].ToString());
        }
    }


 
    public class F2
    {
        [Fact]
        public void  GivenAnExistingEventWithIDAndGuestIDAndPendingInvitationAndMaxParticipantsReached_GuestAcceptsInvitation_ShouldRejectParticipant()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEventFullGuest();
            var guest = GuestFactory.CreateGuest();
            var invitation = activeEvent.InviteGuest(guest._Id);
            activeEvent.AcceptGuestInvitation(guest._Id);
            //create 5 guests and accept them to reach the max participants
            var guest2 = GuestFactory.CreateGuest();
            var guest3 = GuestFactory.CreateGuest();
            var guest4 = GuestFactory.CreateGuest();
            var guest5 = GuestFactory.CreateGuest();
            var guest6 = GuestFactory.CreateGuest();
            activeEvent.InviteGuest(guest2._Id);
            activeEvent.InviteGuest(guest3._Id);
            activeEvent.InviteGuest(guest4._Id);
            activeEvent.InviteGuest(guest5._Id);
            activeEvent.InviteGuest(guest6._Id);
            activeEvent.AcceptGuestInvitation(guest2._Id);
            activeEvent.AcceptGuestInvitation(guest3._Id);
            activeEvent.AcceptGuestInvitation(guest4._Id);
            activeEvent.AcceptGuestInvitation(guest5._Id);
            activeEvent.AcceptGuestInvitation(guest6._Id);
            
            
            
            
            // Act  
            var result = activeEvent.AcceptGuestInvitation(guest._Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventIsFull.ToString(), result.Error.Messages[0].ToString());
        }
    }
    
 
    
    public class F3
    {
        [Fact]
        public void GivenAnExistingEventWithIDAndGuestIDAndPendingInvitationAndEventStatusCancelled_GuestAcceptsInvitation_ShouldRejectParticipant()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.CreateActiveEvent();
            var guest = GuestFactory.CreateGuest();
            activeEvent.CancelEvent();
        
            var invitation = activeEvent.InviteGuest(guest._Id);
         
            
            
            // Act
            var result = activeEvent.AcceptGuestInvitation(guest._Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.CancelledEventCannotBeJoined.ToString(), result.Error.Messages[0].ToString());
        }
    }
    

}