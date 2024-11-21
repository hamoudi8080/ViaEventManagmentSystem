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
            var result = myEvent.InviteGuest(guestId.Id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Contains(myEvent._Invitations,
                i => i._GuestId == guestId.Id && i._InvitationStatus == InvitationStatus.Pending);
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

            var inviteResult = myEvent.InviteGuest(guestId.Id);
            Assert.True(inviteResult.IsSuccess);

            // Act
            myEvent.AcceptGuestInvitation(guestId.Id);

            // Assert
            //Assert.Contains(myEvent._GuestsParticipants, g => g.GuestId == guestId.Id);
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
            var result = myEvent.InviteGuest(guestId.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyActiveOrReadyEventCanBeInvited.ToString(),
                result.Error.Messages[0].ToString());
        }
    }


    public class F2
    {
        [Fact]
        public void GivenFullEventAndGuest_WhenCreatorInvitesGuest_ThenRequestIsRejected()
        {
            // Arrange
            var guestId = GuestFactory.CreateGuest();
            var guestId2 = GuestFactory.CreateGuest();
            var guestId3 = GuestFactory.CreateGuest();
            var guestId4 = GuestFactory.CreateGuest();
            var guestId5 = GuestFactory.CreateGuest();
            var guestId6 = GuestFactory.CreateGuest();
            var guestId7= GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.ReadyEvent();

            var futureevent = DateTime.Now.AddMinutes(30);
            var s = StartDateTime.Create(futureevent);
            myEvent.AddEventStartTime(s.Payload);
            var maxNumber = MaxNumberOfGuests.Create(5);
            myEvent.SetMaxNumberOfGuests(maxNumber.Payload);
            myEvent.MakeEventPublic();
            myEvent.ActivateEvent();


            myEvent.InviteGuest(guestId2.Id);
            myEvent.InviteGuest(guestId3.Id);
            myEvent.InviteGuest(guestId4.Id);
            myEvent.InviteGuest(guestId5.Id);
            myEvent.InviteGuest(guestId6.Id);
            myEvent.InviteGuest(guestId7.Id);

            /*
            guestId2.ReceiveInvitation(inviteResult2.Payload);
            guestId3.ReceiveInvitation(inviteResult3.Payload);
            guestId4.ReceiveInvitation(inviteResult4.Payload);
            guestId5.ReceiveInvitation(inviteResult5.Payload);
            guestId6.ReceiveInvitation(inviteResult6.Payload);

            guestId2.AcceptInvitation(inviteResult2.Payload, myEvent);
            guestId3.AcceptInvitation(inviteResult3.Payload, myEvent);
            guestId4.AcceptInvitation(inviteResult4.Payload, myEvent);
            guestId5.AcceptInvitation(inviteResult5.Payload, myEvent);
            guestId6.AcceptInvitation(inviteResult6.Payload, myEvent);

            */


            myEvent.AcceptGuestInvitation(guestId2.Id);
            myEvent.AcceptGuestInvitation(guestId3.Id);
            myEvent.AcceptGuestInvitation(guestId4.Id);
            myEvent.AcceptGuestInvitation(guestId5.Id);
            myEvent.AcceptGuestInvitation(guestId6.Id);
            myEvent.AcceptGuestInvitation(guestId7.Id);


            // Act
            var result = myEvent.InviteGuest(guestId.Id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventIsFull.ToString(), result.Error.Messages[0].ToString());
        }
    }
}