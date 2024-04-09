using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.UpdateDescriptionTest;

public abstract class UpdateDescriptionTest
{
    public class S1
    {
        [Theory]
        [InlineData(
            "Discuss the impact of technology on modern communication. Include examples of social media, smartphones, and instant messaging on interpersonal interactions and societal dynamics. Address positive and negative effects. ")]
        public void UpdateDescription_ReturnsSuccess(string newDescription)
        {
            // Act
            Result result = EventDescription.Create(newDescription);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }


    public class S2
    {
        [Fact]
        public void UpdateDescription_Success_EmptyDescription()
        {
            // Arrange


            // Act
            Result result = EventDescription.Create("");

            // Assert
            Assert.True(result.IsSuccess); // Ensure the operation succeeded
        }
    }

    public class S3
    {
        [Fact]
        public void UpdateDescription_Success_UpdatesStatusToDraft()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.ReadyEvent(); // Create an event with Ready status
            var originalStatus = viaEvent._EventStatus;
            var newDescription = "New event description";

            var newDescriptionForEvent = EventDescription.Create(newDescription);

            // Act
            var result = viaEvent.UpdateDescription(newDescriptionForEvent.Payload!);

            // Assert
            Assert.True(result.IsSuccess); // Ensure the operation succeeded
            Assert.Equal(newDescription, viaEvent._Description?.Value); // Check that the description has been updated
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); // Check that the event status is now Draft
            Assert.NotEqual(originalStatus, viaEvent._EventStatus); // Ensure that the event status was actually changed
        }
    }

    public class F1
    {
        [Theory]
        [InlineData(
            "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")] // Description with more than 250 characters
        [InlineData(
            "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB")] // Another description with more than 250 characters
        public void UpdateDescription_Failure_ExceedsMaxLength(string newDescription)
        {
            // Act
            Result result = EventDescription.Create(newDescription);

            // Assert
            Assert.False(result.IsSuccess); // Ensure the operation failed
            Assert.Equal(Error.BadRequest(ErrorMessage.DescriptionMustBeBetween0And250Chars),
                result.Error); // Check the specific error returned
        }
    }


    public class F2
    {
        [Theory]
        [InlineData(
            "Discuss the impact of technology on modern communication")]
        public void UpdateDescription_Failure_Cancelled_EventUnmodifiable(string newDescription)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent(); // Create an event with Ready status
            var original_EventStatus = viaEvent._EventStatus;


            var newDescriptionForEvent = EventDescription.Create(newDescription);

            // Act
            var result = viaEvent.UpdateDescription(newDescriptionForEvent.Payload!);

            // Assert

            Assert.False(result.IsSuccess); // Ensure the operation failed
            Assert.Equal(Error.BadRequest(ErrorMessage.CancelledEventCannotBemodified),
                result.Error); // Check the specific error returned
            Assert.Equal(original_EventStatus, viaEvent._EventStatus);
            Assert.Equal("Cancelled event cannot be modified", result.Error.Messages[0].ToString());
        }
    }


    public class F3
    {
        [Theory]
        [InlineData(
            "Discuss the impact of technology on modern communication")]
        public void UpdateDescription_Failure_Active_EventUnmodifiable(string newDescription)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent(); // Create an event with Ready status
            var original_EventStatus = viaEvent._EventStatus;


            var newDescriptionForEvent = EventDescription.Create(newDescription);

            // Act
            var result = viaEvent.UpdateDescription(newDescriptionForEvent.Payload!);

            // Assert

            Assert.False(result.IsSuccess); // Ensure the operation failed
            Assert.Equal(Error.BadRequest(ErrorMessage.ActiveEventCanotBeModified),
                result.Error); // Check the specific error returned
            Assert.Equal(original_EventStatus, viaEvent._EventStatus);
            Assert.Equal("Active event cannot be modified", result.Error.Messages[0].ToString());
        }
    }
}