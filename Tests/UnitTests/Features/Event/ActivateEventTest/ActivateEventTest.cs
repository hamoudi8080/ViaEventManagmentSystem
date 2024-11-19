using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.ActivateEventTest;

public abstract class ActivateEventTest
{
    public class S1
    {
        [Fact]
        public void ActivateEvent_WhenEventInDraftStatus_ShouldMakeEventReadyAndSetActive()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("Event sport title").Payload;
            var description = EventDescription.Create("Training event description").Payload;
            var startDate = StartDateTime.Create(DateTime.Now).Payload;
            var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
            var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
            var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Draft;

            var myEvent = ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
                eventStatus).Payload;
            
            // Act
            var activationResult = myEvent.ActivateEvent();

            // Assert
            Assert.True(activationResult.IsSuccess);
          
            Assert.Equal(EventStatus.Active, myEvent._EventStatus); // Ensure event is active
        }
    }
    
    
    
    
    public class S2
    {
        [Fact]
        public void ActivateEvent_WhenEventInReadyStatus_ShouldMakeEventReadyAndSetActive()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.ReadyEvent();
            
            // Act
            var activationResult = viaEvent.ActivateEvent();

            // Assert
            Assert.True(activationResult.IsSuccess);
          
            Assert.Equal(EventStatus.Active, viaEvent._EventStatus); // Ensure event is active
        }
    }
    
    public class S3
    {
        [Fact]
        public void ActivateEvent_WhenEventInActiveStatus_NothingChangesAndEventSetActive()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("Event sport title").Payload;
            var description = EventDescription.Create("Training event description").Payload;
            var startDate = StartDateTime.Create(DateTime.Now).Payload;
            var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
            var maxNumberOfGuests = MaxNumberOfGuests.Create(40).Payload;
            var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Active;

            var myEvent = ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
                eventStatus).Payload;
            
            // Act
            var activationResult = myEvent.ActivateEvent();

            // Assert
            Assert.True(activationResult.IsSuccess);
          
            Assert.Equal(EventStatus.Active, myEvent._EventStatus); // Ensure event is active
        }
    }
    
    
    
    public class F1
    {
        [Fact]
        public void ActivateEvent_WhenEventInDraftStatusSomeFieldsAreMissingInfo()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("E1").Payload;
            var description = EventDescription.Create("Tra").Payload;
            var startDate = StartDateTime.Create(DateTime.Now.AddHours(1)).Payload;
            var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
            var maxNumberOfGuests = MaxNumberOfGuests.Create(700).Payload;
            var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Draft;

            var viaevent = ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
                eventStatus).Payload;
            
            // Act
            var activationResult = viaevent.ActivateEvent();

            // Assert
            Assert.False(activationResult.IsSuccess);
            Assert.Contains(activationResult.ErrorCollection!, error => 
                error.Messages.Any(message => message.DisplayName == ErrorMessage.MaxGuestsNoMustBeWithin5and50.DisplayName));
             
        }
    }
    
    
    
    public class F2
    {
        [Fact]
        public void CancelledEvent_WhenCreaterActivateEvent_CancelledEventCannotBeActivated()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("E1").Payload;
            var description = EventDescription.Create("Tra").Payload;
            var startDate = StartDateTime.Create(DateTime.Now.AddHours(1)).Payload;
            var endDate = EndDateTime.Create(DateTime.Now.AddHours(3)).Payload;
            var maxNumberOfGuests = MaxNumberOfGuests.Create(700).Payload;
            var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Cancelled;

            var viaevent = ViaEvent.Create(id, title, description, startDate, endDate, maxNumberOfGuests, eventVisibility,
                eventStatus).Payload;
            
            // Act
            var activationResult = viaevent.ActivateEvent();

            // Assert
            Assert.False(activationResult.IsSuccess);
            Assert.Contains(ErrorMessage.CancelledEventCannotBeActivated.DisplayName,
                activationResult.Error.Messages[0].DisplayName); 
            
             
        }
    }
}