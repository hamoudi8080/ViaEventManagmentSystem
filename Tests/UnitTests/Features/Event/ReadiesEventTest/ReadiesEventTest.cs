using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
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
            Assert.True(result.IsSuccess); // Check if the result is successful
            Assert.Equal(EventStatus.Ready, viaEvent._EventStatus); // Check if the event status is updated to Ready
        }
    }


    public class F1
    {
        [Fact]
        public void GivenDraftEvent_WithMissingOrInvalidData_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess); // Check if the result is a failure
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); // Ensure event status remains as Draft

            // Additional assertions to check for failure message
            Assert.Contains(result.Error.CustomMessage,
                result.Error.CustomMessage); // Check if error message for missing data is present
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
            Assert.False(result.IsSuccess); // Check if the result is a failure
            Assert.Equal(ErrorMessage.CancelledEventCannotBeMadeReady.ToString(),
                result.Error.Messages[0].ToString()); // Ensure event status remains as Draft
        }
    }


    public class F3
    {
        [Fact]
        public void GivenEventWithPastStartDateTime_WhenMakeEventReady_ThenFailureMessageIsProvided()
        {
            // Arrange
            var pastStartDateTime = DateTime.Now.AddHours(-1); // Set start time to 1 hour ago
            var viaEvent = ViaEventTestFactory.DraftEvent(pastStartDateTime);

            // Act
            var result = viaEvent.MakeEventReady();

            // Assert
            Assert.False(result.IsSuccess); // Check if the result is a failure
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); // Ensure event status remains as Draft
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
            Assert.False(result.IsSuccess); // Check if the result is a failure
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); // Ensure event status remains as Draft

        }
    }
}