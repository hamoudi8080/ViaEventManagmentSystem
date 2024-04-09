using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPrivateTest;

public abstract class MakeEventPrivateTest
{
    public class S1
    {
        [Fact]
        public void  MakeEventPrivate_ExistingEventWithValidStatus_EventIsMadePublicAndStatusIsUnchanged()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();
            viaEvent.MakeEventReady(); // Set event status to Ready

            // Act
            var result = viaEvent.MakeEventPrivate();

            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful

            // Check if the event remains private
            Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);

            // Check if the event remains in its current status (Ready)
            Assert.Contains(viaEvent._EventStatus, new[] { EventStatus.Draft, EventStatus.Ready });
        }
    }


    public class S2
    {
        [Fact]
        public void MakeEventPrivate_PublicEvent_SuccessfullyMadePrivateAndStatusIsDraft()
        
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.ReadyEvent();
            
            
            // Act
            var result = viaEvent.MakeEventPrivate();
            
            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful

            // Check if the event is made private
            Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);

            // Check if the status is changed to Draft
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
           
        }
    }
    
    public class F1
    {
        [Fact]
        public void MakeEventPrivate_ActiveEvent_FailureMessageProvided()
        
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent();
            
            
            // Act
            var result = viaEvent.MakeEventPrivate();
            
            // Assert
            Assert.False(result.IsSuccess);  
            // Check if the failure message is provided as expected
            Assert.Contains(ErrorMessage.ActiveEventCannotBePrivate.ToString(), result.Error.Messages[0].ToString());
           
        }
    }
    
    public class F2
    {
        [Fact]
        public void MakeEventPrivate_ActiveEvent_FailureMessageProvided()
        
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent();
            
            
            // Act
            var result = viaEvent.MakeEventPrivate();
            
            // Assert
            Assert.False(result.IsSuccess);  
            // Check if the failure message is provided as expected
            Assert.Contains(ErrorMessage.CancelledEventCannotBemodified.ToString(), result.Error.Messages[0].ToString());
           
        }
    }
    
    
}