using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.UpdateDescriptionTest;

public class UpdateDescriptionCommandTest
{
    
    public class UpdateDescriptionCommandTests
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenEventIdAndEventDescriptionAreValid()
        {
            // Arrange
            string validEventDescription = "This is a valid event description.";

            // Act
            var result = UpdateDescriptionCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(), validEventDescription);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validEventDescription, result.Payload.Description.Value);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
        {
            // Arrange
            string invalidEventId = ""; // Assuming empty string is invalid
            string validEventDescription = "This is a valid event description.";

            // Act
            var result = UpdateDescriptionCommand.Create(invalidEventId, validEventDescription);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEventDescriptionIsInvalid()
        {
            // Arrange
            string validEventId = "validEventId";
            string invalidEventDescription = ""; // Assuming empty string is invalid

            // Act
            var result = UpdateDescriptionCommand.Create(validEventId, invalidEventDescription);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}