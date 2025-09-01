using UnitTests.Common.Factories.EventFactory;
using UnitTests.Common.Factories;
using UnitTests.Common.Factories.GuestFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.GuestTests.GuestCancelsParticipationTest;

public abstract class GuestCancelsParticipationTest
{
    public class S1
    {   
        [Fact]
        public void GivenGuestId_GuestCancels_EventRemovesGuestFromParticipation()
        {
            //Arrange
            var guest1Id = GuestFactory.CreateGuest();
            var myEvent = ViaEventTestFactory.ReadyEvent();
            //myEvent.MakeEventPublic();
            myEvent.ActivateEvent();
            myEvent.InviteGuest(guest1Id.Id);
            
            //Act
            var result = myEvent.CancelGuestParticipation(guest1Id.Id);
            
            
            
            //Assert
            Assert.True(result.IsSuccess);
        }
    }
    
    
    public class S2
    {   
        [Fact]
        public void GivenGuestId_GuestCancels_GuestIsNotMarkedAsParticipantNothingHappens()
        {
            //Arrange
            var guest1Id = GuestFactory.CreateGuest();
            var guest2Id = GuestFactory.CreateGuest();
            DateTime dateTime = DateTime.Now.AddHours(2);
            DateTime enddateTime = DateTime.Now.AddHours(6);
            var eventstartTime = StartDateTime.Create(dateTime);
            var eventEndTime = EndDateTime.Create(enddateTime);
            
            
            var myEvent = ViaEventTestFactory.ReadyEvent();
            myEvent.MakeEventPublic();
            myEvent.ActivateEvent();
            myEvent.AddEventStartTime(eventstartTime.Payload);
            myEvent.EventEndTime(eventEndTime.Payload);
            
            //Act
            var result = myEvent.CancelGuestParticipation(guest2Id.Id);
            
            //Assert
            Assert.True(result.IsSuccess);
        }
    }
    
    
    public class F1
    {   
        [Fact]
        public void GivenGuestId_GuestCancels_GuestIsNotMarkedAsParticipantNothingHappens()
        {
            //Arrange
            var guest2Id = GuestFactory.CreateGuest();
            DateTime dateTime = DateTime.Now.AddHours(-2);
            DateTime enddateTime = DateTime.Now.AddHours(6);
            var eventstartTime = StartDateTime.Create(dateTime);
            var eventEndTime = EndDateTime.Create(enddateTime);
            
           
            var myEvent = ViaEventTestFactory.ReadyEvent();
            myEvent.AddGuestParticipation(guest2Id.Id);
            myEvent.AddEventStartTime(eventstartTime.Payload);
            myEvent.EventEndTime(eventEndTime.Payload);
            
            //Act
            var result = myEvent.CancelGuestParticipation(guest2Id.Id);
            
            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.CancelParticipationRejected.ToString(), result.Error.Messages[0].ToString());
        }
    }
}