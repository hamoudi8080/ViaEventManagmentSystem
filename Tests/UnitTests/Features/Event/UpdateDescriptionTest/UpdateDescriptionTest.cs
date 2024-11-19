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
            // Arrange
            var dResult = EventDescription.Create(newDescription);
            var viaEvent = ViaEventTestFactory.DraftEvent();
            
            // Act
            var result = viaEvent.UpdateDescription(dResult.Payload);

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
            var dResult = EventDescription.Create("");
            var viaEvent = ViaEventTestFactory.DraftEvent();

            // Act
            var result = viaEvent.UpdateDescription(dResult.Payload);

            // Assert
            Assert.True(result.IsSuccess); 
        }
    }

    public class S3
    {
        [Theory]
        [InlineData("New event description")]
        [InlineData("")]
        public void UpdateDescription_Success_UpdatesStatusToDraft(string descriptions)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.ReadyEvent(); 
            var originalStatus = viaEvent._EventStatus;
            var newDescriptionForEvent = EventDescription.Create(descriptions);

            // Act
            var result = viaEvent.UpdateDescription(newDescriptionForEvent.Payload);

            // Assert
            Assert.True(result.IsSuccess); 
            Assert.Equal(descriptions, viaEvent._Description?.Value); 
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
            Assert.NotEqual(originalStatus, viaEvent._EventStatus); 
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
            Assert.Equal(ErrorMessage.DescriptionMustBeBetween0And250Chars.DisplayName, result.Error.Messages[0].DisplayName);
            
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
            var dResult = EventDescription.Create(newDescription);
            var viaEvent = ViaEventTestFactory.DraftEvent();
            viaEvent.CancelEvent();

            // Act
            var result = viaEvent.UpdateDescription(dResult.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Ensure the operation failed
            Assert.Equal(ErrorMessage.CancelledEventCannotBemodified.DisplayName, result.Error.Messages[0].DisplayName);
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
            var dResult = EventDescription.Create(newDescription);
            var viaEvent = ViaEventTestFactory.DraftEvent();
            viaEvent.ActivateEvent();

            // Act
            var result = viaEvent.UpdateDescription(dResult.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Ensure the operation failed
            Assert.Equal(ErrorMessage.ActiveEventCanotBeModified.DisplayName, result.Error.Messages[0].DisplayName);
 
        }
    }
}