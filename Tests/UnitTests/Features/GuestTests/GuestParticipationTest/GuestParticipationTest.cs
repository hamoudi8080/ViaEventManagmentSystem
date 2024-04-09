using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
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
            var guest = GuestFactory.CreateGuest()._Id;


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
            var guest = GuestFactory.CreateGuest()._Id;

            // Act
            var result = draftEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyActiveEventCanBeJoined.ToString(), result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
        }
    }
    
    /*
    public class F2
    {
          [Fact]
            public void GivenExistingValidEventWithIDAndEventStatusDraft_GuestChoosesToParticipate_ShouldRejectParticipant()
            {
                //arrange
                ViaEvent Event = ViaEventTestFactory.ReadyEvent();
                Event.ActivateEvent();
                Event.MakeEventPublic();
                
                var guest = GuestFactory.GuestFactory.CreateGuest()._Id;

                // Act
                var result = Event.AddGuestParticipation(guest);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Contains("InvalidInputError", result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
            }
         
    }
    */
    
    public class F3
    {
        [Fact]
        public void GivenExistingValidEventWithStartTimeInThePast_GuestChoosesToParticipate_ShouldRejectParticipantWithPastEventMessage()
        {
            // Arrange
            var pastStartTime = DateTime.Now.AddMinutes(-30); // Set start time in the past
            var s = StartDateTime.Create(pastStartTime);
            ViaEvent readyEvent = ViaEventTestFactory.ReadyEvent();
            readyEvent.ActivateEvent();
            readyEvent.MakeEventPublic();
            readyEvent.AddEventStartTime(s.Payload);
            
            var guest = GuestFactory.CreateGuest()._Id;

            // Act
            var result = readyEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(ErrorMessage.CannotParticipatedInStartedEvent.ToString(), result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
        }
    }
    
    public class F4
    {
        [Fact]
        public void GivenExistingValidEventWithPrivateVisibility_GuestChoosesToParticipate_ShouldRejectParticipantWithOnlyPublicEventsMessage()
        {
            // Arrange
            ViaEvent privateEvent = ViaEventTestFactory.PrivateEvent();
            var guest = GuestFactory.CreateGuest()._Id;

            // Act
            var result = privateEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.OnlyPublicEventCanBeParticipated.ToString(), result.Error.Messages[0].ToString()); // Ensure the proper error message is returned
        }
    }
    
    public class F5
    {
        [Fact]
        public void GivenGuestAlreadyParticipantAtEvent_GuestChoosesToParticipate_ShouldRejectParticipantWithMultipleSlotsMessage()
        {
            // Arrange
            
            //arrange
            var guest = GuestFactory.CreateGuest()._Id;
            ViaEvent readyEvent = ViaEventTestFactory.ReadyEvent();
            readyEvent.ActivateEvent();
            readyEvent.MakeEventPublic();
            var futureevent = DateTime.Now.AddMinutes(30);
            var s = StartDateTime.Create(futureevent);
            readyEvent.AddEventStartTime(s.Payload);
            readyEvent.AddGuestParticipation(guest);
         
    
             

            // Act
            var result = readyEvent.AddGuestParticipation(guest);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.GuestAlreadyParticipantAtEvent.ToString(), result.Error.Messages[0].ToString());// Ensure the proper error message is returned
            
             
            
            
        }
    }
}
    
   
 