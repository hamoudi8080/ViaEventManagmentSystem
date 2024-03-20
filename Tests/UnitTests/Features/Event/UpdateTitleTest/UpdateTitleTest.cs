using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Features.Event.UpdateTitleTest
{
    public abstract class UpdateTitleTest
    {
        public class S1
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Success_DraftStatus(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateEvent();

                // Act
                var result = viaEvent.UpdateTitle(newTitle);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.Equal(newTitle, viaEvent._EventTitle.Value);
                Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
            }
        }

        public class S2
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Success_ReadyStatus(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.ReadyEvent(); // Create an event with Ready status
                var originalStatus = viaEvent._EventStatus;

                // Act
                var result = viaEvent.UpdateTitle(newTitle);

                // Assert
                Assert.True(result.IsSuccess); // Ensure the operation succeeded
                Assert.Equal(newTitle, viaEvent._EventTitle.Value); // Check that the title has been updated
                // Check that the title has been updated
                Assert.Equal(EventStatus.Draft, viaEvent._EventStatus); // Check that the event status is now Draft
                Assert.NotEqual(originalStatus,
                    viaEvent._EventStatus); // Ensure that the event status was actually changed
            }
        }

        public class F1
        {
            [Theory]
            [InlineData("")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.ReadyEvent();

                // Act
                var result = viaEvent.UpdateTitle(newTitle);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Event title must be between 3 and 75 characters", result.Error.Messages[0].ToString());
            }
        }

        public class F2
        {
            [Theory]
            [InlineData("XY")]
            [InlineData("A")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateEvent();

                // Act
                var result = viaEvent.UpdateTitle(newTitle);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Event title must be between 3 and 75 characters", result.Error.Messages[0].ToString());
            }
        }

        public class F3
        {
            [Theory]
            [InlineData(
                "This is a very long title that exceeds the maximum character limit of 75 characters. This title is definitely too long.")]
            public void UpdateTitle_Failure_EmptyTitle(string newTitle)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateEvent();

                // Act
                var result = viaEvent.UpdateTitle(newTitle);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Event title must be between 3 and 75 characters", result.Error.Messages[0].ToString());
            }
        }

        public class F4
        {
            [Fact]
            public void UpdateTitle_Failure_NullTitle()
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateEvent();

                // Act
                var result = viaEvent.UpdateTitle(null);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("Event title must be between 3 and 75 characters", result.Error.Messages[0].ToString());
            }
        }

        public class F5
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Failure_ActiveEvent(string title)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CreateActiveEvent();
                ;


                // Act
                var result = viaEvent.UpdateTitle(title);

                // Assert
                Assert.False(result.IsSuccess);

                Assert.Equal("Active event cannot be modified", result.Error.Messages[0].ToString());
            }
        }

        public class F6
        {
            [Theory]
            [InlineData("Scary Movie Night!")]
            [InlineData("Graduation Gala")]
            [InlineData("VIA Hackathon")]
            public void UpdateTitle_Failure_CancelledEvent(string title)
            {
                // Arrange
                var viaEvent = ViaEventTestFactory.CancelledEvent(); // Create a cancelled event


                // Act
                var result = viaEvent.UpdateTitle(title);

                // Assert
                Assert.False(result.IsSuccess); // Ensure the operation failed
                Assert.Equal("Cancelled event cannot be modified",
                    result.Error.Messages[0].ToString()); // Check the error message
            }
        }
    }
}