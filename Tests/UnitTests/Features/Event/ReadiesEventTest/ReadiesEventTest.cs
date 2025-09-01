using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.ReadiesEventTest;

public abstract class ReadiesEventTest
{
    public class S1
    {
        [Fact]
        public void GivenDraftEvent_WithValidData_WhenMakeEventReady_ThenEventIsMadeReady()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.DraftEvent();

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.True(result.IsSuccess); 
            Assert.Equal(EventStatus.Ready, viaEvent._EventStatus);
        }
    }


    public class F1
    {
        [Fact]
        public void GivenDraftEvent_WithMissingOrInvalidData_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("");
            var description = EventDescription.Create("Training event description");
            //var startDate = StartDateTime.Create(DateTime.Now.AddHours(1));
            //var endDate = EndDateTime.Create(DateTime.Now.AddHours(3));
            var maxNumberOfGuests = MaxNumberOfGuests.Create(2);
            //var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Draft;

            var viaEvent = ViaEvent.Create(id, title.Payload, description.Payload, null, null, maxNumberOfGuests.Payload, null,
                eventStatus).Payload;

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess); 
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
            Assert.NotNull(result.ErrorCollection);
            Assert.NotEmpty(result.ErrorCollection);
            
        }
    }

    public class F2
    {
        [Fact]
        public void GivenCancelledEvent_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent();

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.ErrorCollection!, error => 
                error.Messages.Any(message => message.DisplayName == ErrorMessage.CancelledEventCannotBeMadeReady.DisplayName));
           
            
        }
    }


    public class F3
    {
        [Fact]
        public void GivenEventWithPastStartDateTime_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var id = EventId.Create().Payload;
            var title = EventTitle.Create("Sport");
            var description = EventDescription.Create("Training event description");
            var startDate = StartDateTime.Create(DateTime.Now.AddHours(-1));
           var endDate = EndDateTime.Create(DateTime.Now.AddHours(3));
            var maxNumberOfGuests = MaxNumberOfGuests.Create(2);
            var eventVisibility = EventVisibility.Public;
            var eventStatus = EventStatus.Draft;

            var viaEvent = ViaEvent.Create(id, title.Payload, description.Payload, startDate.Payload, endDate.Payload, maxNumberOfGuests.Payload, null,
                eventStatus).Payload;

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess); 
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); 
            Assert.Contains(result.ErrorCollection!, error => 
                error.Messages.Any(message => message.DisplayName == ErrorMessage.EventInThePastCannotBeReady.DisplayName));

            
        }
    }

    public class F4
    {
        [Fact]
        public void GivenEventWithDefaultTitle_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Default title is "Working Title"

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess); 
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); 
            Assert.Contains(result.ErrorCollection!, error => 
                error.Messages.Any(message => message.DisplayName == ErrorMessage.TitleMustbeChangedFromDefault.DisplayName));

            

        }
    }
}