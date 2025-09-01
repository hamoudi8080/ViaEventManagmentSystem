using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
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
            Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);
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
            Assert.True(result.IsSuccess);
            Assert.Equal(EventVisibility.Private, viaEvent._EventVisibility);
            Assert.Equal(EventStatus.Ready, viaEvent._EventStatus);
           
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
            Assert.Contains(ErrorMessage.ActiveEventCannotBePrivate.DisplayName, result.Error.Messages[0].DisplayName);
           
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
            Assert.Contains(ErrorMessage.CancelledEventCannotBemodified.DisplayName, result.Error.Messages[0].DisplayName);
           
        }
    }
    
    
}