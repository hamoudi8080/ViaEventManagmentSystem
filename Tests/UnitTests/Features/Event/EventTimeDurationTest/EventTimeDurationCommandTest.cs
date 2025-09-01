using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;

namespace UnitTests.Features.Event.EventTimeDurationTest;

public class EventTimeDurationCommandTest
{

    public class EventTimeDurationCommandTests
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenEventIdAndEventTimeAreValid()
        {
            // Arrange
            DateTime validStartDateTime = DateTime.Now;
            DateTime validEndDateTime = DateTime.Now.AddHours(1);

            // Act
            var result = EventTimeDurationCommand.Create(ViaEventTestFactory.ValidEventId().Value.ToString(),
                validStartDateTime, validEndDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validStartDateTime, result.Payload.StartDateTime.Value);
            Assert.Equal(validEndDateTime, result.Payload.EndDateTime.Value);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEventIdIsInvalid()
        {
            // Arrange
            string invalidEventId = ""; // Assuming empty string is invalid
            DateTime validStartDateTime = DateTime.Now;
            DateTime validEndDateTime = DateTime.Now.AddHours(1);

            // Act
            var result = EventTimeDurationCommand.Create(invalidEventId, validStartDateTime, validEndDateTime);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEventTimeIsInvalid()
        {
            // Arrange
            string validEventId = "validEventId";
            DateTime invalidStartDateTime = DateTime.Now.AddHours(1); // Assuming start time is after end time
            DateTime invalidEndDateTime = DateTime.Now;

            // Act
            var result = EventTimeDurationCommand.Create(validEventId, invalidStartDateTime, invalidEndDateTime);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}