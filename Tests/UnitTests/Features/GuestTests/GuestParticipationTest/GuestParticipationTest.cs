using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestParticipationTest;

public abstract class GuestParticipationTest
{
    public class S1
    {
        [Fact]
        public void GivenReadyEvent_WhenGuestAttendPublic_EventRegisteredTheGuestSuccessfully()
        {
            //arrange
            ViaEvent readyEvent = ViaEventTestFactory.ReadyEvent();
            readyEvent.ActivateEvent();
            readyEvent.MakeEventPublic();
            var futureevent = DateTime.Now.AddMinutes(30);
            var s = StartDateTime.Create(futureevent);
            readyEvent.AddEventStartTime(s.Payload);
            var guest = GuestFactory.CreateGuest().Id;


            //act
            var result = readyEvent.AddGuestParticipation(guest);


            //assert 
            Assert.True(result.IsSuccess);
        }
    }

    public class F1
    {
        [Fact]
        public void GivenExistingValidEventWithIDAndEventStatusDraft_GuestChoosesToParticipate_ShouldRejectParticipant()
        {
            // Arrange
            ViaEvent draftEvent = ViaEventTestFactory.DraftEvent();
            draftEvent.MakeEventPublic();
            var guest = GuestFactory.CreateGuest().Id;

            // Act
            var result = draftEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyActiveEventCanBeJoined.ToString(), result.Error.Messages[0].ToString());
        }
    }


    public class F2
    {
        [Fact]
        public void GivenExistingValidEventWithIDAndEventStatusDraft_GuestChoosesToParticipate_ShouldRejectParticipant()
        {
            //arrange
            ViaEvent Event = ViaEventTestFactory.ReadyEvent();
            var maxNumberOfGuests = MaxNumberOfGuests.Create(5);
            Event.SetMaxNumberOfGuests(maxNumberOfGuests.Payload);
            Event.ActivateEvent();
            Event.MakeEventPublic();
            var guest1 = GuestFactory.CreateGuest().Id;
            var guest2 = GuestFactory.CreateGuest().Id;
            var guest3 = GuestFactory.CreateGuest().Id;
            var guest4 = GuestFactory.CreateGuest().Id;
            var guest5 = GuestFactory.CreateGuest().Id;
            var guest6 = GuestFactory.CreateGuest().Id;


            // Act
            Event.AddGuestParticipation(guest1);
            Event.AddGuestParticipation(guest2);
            Event.AddGuestParticipation(guest3);
            Event.AddGuestParticipation(guest4);
            Event.AddGuestParticipation(guest5);
            var g6 = Event.AddGuestParticipation(guest6);


            // Assert
            Assert.False(g6.IsSuccess);
            Assert.Equal(ErrorMessage.EventIsFull.DisplayName, g6.Error.Messages[0].DisplayName);
        }
    }


    public class F3
    {
        [Fact]
        public void
            GivenExistingValidEventWithStartTimeInThePast_GuestChoosesToParticipate_ShouldRejectParticipantWithPastEventMessage()
        {
            // Arrange
            var pastStartTime = DateTime.Now.AddMinutes(-30); // Set start time in the past
            var s = StartDateTime.Create(pastStartTime);
            ViaEvent readyEvent = ViaEventTestFactory.ReadyEvent();
            readyEvent.ActivateEvent();
            readyEvent.MakeEventPublic();
            readyEvent.AddEventStartTime(s.Payload);

            var guest = GuestFactory.CreateGuest().Id;

            // Act
            var result = readyEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(ErrorMessage.CannotParticipatedInStartedEvent.ToString(),
                result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
        }
    }

    public class F4
    {
        [Fact]
        public void
            GivenExistingValidEventWithPrivateVisibility_GuestChoosesToParticipate_ShouldRejectParticipantWithOnlyPublicEventsMessage()
        {
            // Arrange
            ViaEvent privateEvent = ViaEventTestFactory.PrivateEvent();
            var guest = GuestFactory.CreateGuest().Id;

            // Act
            var result = privateEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyPublicEventCanBeParticipated.ToString(),
                result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
        }
    }

    public class F5
    {
        [Fact]
        public void
            GivenGuestAlreadyParticipantAtEvent_GuestChoosesToParticipate_ShouldRejectParticipantWithMultipleSlotsMessage()
        {
            // Arrange
            ViaEvent Event = ViaEventTestFactory.ReadyEvent();
            Event.ActivateEvent();
            Event.MakeEventPublic();
            var guest1 = GuestFactory.CreateGuest().Id;
            var guest2 = GuestFactory.CreateGuest().Id;
            var guest3 = GuestFactory.CreateGuest().Id;
            var guest4 = GuestFactory.CreateGuest().Id;
            var guest5 = GuestFactory.CreateGuest().Id;
            var guest6 = GuestFactory.CreateGuest().Id;


            // Act
            Event.AddGuestParticipation(guest1);
            Event.AddGuestParticipation(guest2);
            Event.AddGuestParticipation(guest3);
            Event.AddGuestParticipation(guest4);
            Event.AddGuestParticipation(guest5);
            Event.AddGuestParticipation(guest6);
            var g6 = Event.AddGuestParticipation(guest6);
            

            // Assert
            Assert.False(g6.IsSuccess);
            Assert.Equal(ErrorMessage.GuestAlreadyParticipantAtEvent.DisplayName,
                g6.Error.Messages[0].ToString()); 
        }
    }
}