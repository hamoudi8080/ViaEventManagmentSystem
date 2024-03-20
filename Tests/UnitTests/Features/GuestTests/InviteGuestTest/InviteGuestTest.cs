using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using Xunit.Sdk;
using ErrorMessage = ViaEventManagmentSystem.Core.Tools.OperationResult.ErrorMessage;

namespace UnitTests.Features.GuestTests.InviteGuestTest;

public abstract class InviteGuestTest
{
    public class S1
    {
        [Fact]
        public void GivenEventAndGuest_WhenCreatorInvitesGuest_ThenPendingInvitationIsRegistered()
        {
            // Arrange
            var eventId = ViaEventTestFactory.CreateEvent();
            var guestId = GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.ReadyEvent();
            myEvent.MakeEventPublic();
            myEvent.ActivateEvent();

            // Act
            var result = myEvent.InviteGuest(guestId._Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Contains(myEvent._Invitations,
                i => i._GuestId == guestId._Id && i._InvitationStatus == InvitationStatus.Pending);
        }
    }


    public class S2
    {
        [Fact]
        public void GivenPendingInvitation_WhenGuestAccepts_ThenGuestIsAddedToParticipationList()
        {
            // Arrange

            var guestId = GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.ReadyEvent();
            myEvent.MakeEventPublic();
            myEvent.ActivateEvent();
            var futureevent = DateTime.Now.AddMinutes(30);
            var s = StartDateTime.Create(futureevent);
            myEvent.AddEventStartTime(s.Payload);

            var inviteResult = myEvent.InviteGuest(guestId._Id);
            Assert.True(inviteResult.IsSuccess);
            var invitation = inviteResult.Payload;


            guestId.ReceiveInvitation(invitation);

            // Act
            guestId.AcceptInvitation(invitation, myEvent);

            // Assert
            Assert.Contains(myEvent._GuestsParticipants, g => g == guestId._Id);
        }
    }

    public class F1
    {
        [Fact]
        public void GivenDraftEventAndGuest_WhenCreatorInvitesGuest_ThenRequestIsRejected()
        {
            // Arrange
            var eventId = ViaEventTestFactory.CreateEvent();
            var guestId = GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.DraftEvent();

            // Act
            var result = myEvent.InviteGuest(guestId._Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyActiveOrReadyEventCanBeInvited.ToString(),
                result.Error.Messages[0].ToString());
        }
    }

    /*
    public class F2
    {

        [Fact]
        public void GivenFullEventAndGuest_WhenCreatorInvitesGuest_ThenRequestIsRejected()
        {
            // Arrange
            var eventId = ViaEventTestFactory.CreateEvent();
            var guestId = GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.FullEvent(eventId);
            myEvent.MakeEventPublic();
            myEvent.ActivateEvent();

            // Act
            var result = myEvent.InviteGuest(guestId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("You cannot invite guests if the event is full", result.Error);
        }
        
    }
    */
}