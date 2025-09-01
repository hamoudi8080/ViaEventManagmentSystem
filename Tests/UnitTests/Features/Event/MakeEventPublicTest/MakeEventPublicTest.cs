using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.MakeEventPublic;

public abstract class MakeEventPublicTest
{
    public class S1
    {
        [Fact]
        public void MakeEventPublic_ExistingEventWithValidStatus_EventIsMadePublicAndStatusIsUnchanged()
        {
            // Arrange

            var viaEvent = ViaEventTestFactory.CreateEvent();

            // Act
            var result = viaEvent.MakeEventPublic();

            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful
            Assert.Equal(EventVisibility.Public, viaEvent._EventVisibility);
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
        }
    }


    public class F2
    {
        [Fact]
        public void MakeEventPublic_CancelledEvent_FailureMessageProvided()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent();
            
            // Act
            var result = viaEvent.MakeEventPublic();

            // Assert
            Assert.False(result.IsSuccess);  
            Assert.Contains(ErrorMessage.CancelledEventCannotBePublic.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }
}