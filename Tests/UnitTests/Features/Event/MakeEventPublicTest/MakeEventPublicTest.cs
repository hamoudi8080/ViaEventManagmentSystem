using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
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

            // Check if the event is public
            Assert.Equal(EventVisibility.Public, viaEvent._EventVisibility);

            // Check if the status is unchanged (assuming it's set to Draft initially)
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
            Assert.False(result.IsSuccess); // Check if the operation failed

            // Check if the failure message is provided as expected
            Assert.Contains(ErrorMessage.CancelledEventCannotBePublic.ToString(), result.Error.Messages[0].ToString());
        }
    }
}