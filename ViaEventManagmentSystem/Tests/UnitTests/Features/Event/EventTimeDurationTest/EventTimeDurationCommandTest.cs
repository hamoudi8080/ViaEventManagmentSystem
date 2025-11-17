using UnitTests.Common.Factories.EventFactory;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.EventTimeDurationTest;

public class EventTimeDurationCommandTest
{
    public class EventTimeDurationCommandTests
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenEventIdAndEventTimeAreValid()
        {
            // Arrange
            var validStartDateTime = DateTime.Now;
            var validEndDateTime = DateTime.Now.AddHours(1);

            // Act
            EventId eventId = ViaEventTestFactory.ValidEventId();
            var result = EventTimeDurationCommand.Create(eventId.Value.ToString(),
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
            var invalidEventId = ""; // Assuming empty string is invalid
            var validStartDateTime = DateTime.Now;
            var validEndDateTime = DateTime.Now.AddHours(1);

            // Act
            var result = EventTimeDurationCommand.Create(invalidEventId, validStartDateTime, validEndDateTime);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Create_ShouldReturnFailure_WhenEventTimeIsInvalid()
        {
            // Arrange
            var validEventId = "validEventId";
            var invalidStartDateTime = DateTime.Now.AddHours(1); // Assuming start time is after end time
            var invalidEndDateTime = DateTime.Now;

            // Act
            var result = EventTimeDurationCommand.Create(validEventId, invalidStartDateTime, invalidEndDateTime);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}