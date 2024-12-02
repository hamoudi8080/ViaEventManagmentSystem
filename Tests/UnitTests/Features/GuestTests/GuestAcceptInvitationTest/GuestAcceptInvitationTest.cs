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
            var invitation = activeEvent.InviteGuest(guest.Id);


            // Act
            var result = activeEvent.AcceptGuestInvitation(guest.Id);

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
            var guest2 = GuestFactory.CreateGuest();
            activeEvent.InviteGuest(guest.Id);
            
            // Act
            var result = activeEvent.AcceptGuestInvitation(guest2.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.GuestIsNotInvitedToEvent.ToString(), result.Error.Messages[0].ToString());
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
            var invitation = activeEvent.InviteGuest(guest.Id);
            activeEvent.AcceptGuestInvitation(guest.Id);
            //create 5 guests and accept them to reach the max participants
            var guest2 = GuestFactory.CreateGuest();
            var guest3 = GuestFactory.CreateGuest();
            var guest4 = GuestFactory.CreateGuest();
            var guest5 = GuestFactory.CreateGuest();
            var guest6 = GuestFactory.CreateGuest();
            activeEvent.InviteGuest(guest2.Id);
            activeEvent.InviteGuest(guest3.Id);
            activeEvent.InviteGuest(guest4.Id);
            activeEvent.InviteGuest(guest5.Id);
            activeEvent.InviteGuest(guest6.Id);
            activeEvent.AcceptGuestInvitation(guest2.Id);
            activeEvent.AcceptGuestInvitation(guest3.Id);
            activeEvent.AcceptGuestInvitation(guest4.Id);
            activeEvent.AcceptGuestInvitation(guest5.Id);
            activeEvent.AcceptGuestInvitation(guest6.Id);
            
            
            
            
            // Act  
            var result = activeEvent.AcceptGuestInvitation(guest.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventIsFull.DisplayName, result.Error.Messages[0].DisplayName);
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
            activeEvent.InviteGuest(guest.Id);
            activeEvent.CancelEvent();
          
         
            
            
            // Act
            var result = activeEvent.AcceptGuestInvitation(guest.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.CancelledEventCannotBeJoined.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }
    
    
    public class  F4
    {
        [Fact]
        public void ReadyEvent_RegisteredGuest_PendingInvitation_ThenGuestAcceptInvitation_Failure()
        {
            // Arrange
            var activeEvent = ViaEventTestFactory.ReadyEvent();
            var guest = GuestFactory.CreateGuest();
            activeEvent.InviteGuest(guest.Id);
            
            
            // Act
            var result = activeEvent.AcceptGuestInvitation(guest.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventCannotYetBeJoined.DisplayName, result.Error.Messages[0].DisplayName);
        }
        
        
    }
}