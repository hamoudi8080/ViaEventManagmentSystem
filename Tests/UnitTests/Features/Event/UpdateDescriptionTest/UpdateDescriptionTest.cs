using UnitTests.Features.Event.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

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
            var viaEvent = ViaEventTestFactory.CreateEvent();

            // Act
            var result = viaEvent.UpdateDescription(newDescription);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newDescription, viaEvent._Description.Value);
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
        }
    }


    public class S2
    {
        [Fact]
        public void UpdateDescription_Success_EmptyDescription()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Create an event
            var originalDescription = viaEvent._Description?.Value; // Store the original description

            // Act
            var result = viaEvent.UpdateDescription(""); // Set the description to empty string

            // Assert
            Assert.True(result.IsSuccess); // Ensure the operation succeeded
            Assert.Equal("",
                viaEvent._Description?.Value); // Check that the description has been updated to empty string
            Assert.Equal("", originalDescription);
        }
    }
}