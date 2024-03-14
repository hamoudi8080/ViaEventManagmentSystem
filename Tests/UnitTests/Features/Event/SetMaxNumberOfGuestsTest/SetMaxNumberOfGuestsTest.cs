using UnitTests.Features.Event.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.SetMaxNumberOfGuestsTest;

public abstract class SetMaxNumberOfGuestsTest
{
    public class S1
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithDraftStatusAndValidNumberOfGuests_Success(int numberOfGuests)
        {
            // Arrange

            var viaEvent = ViaEventTestFactory.CreateEvent();
            var maxNoOfGuest = MaxNumberOfGuests.Create(numberOfGuests);

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(maxNoOfGuest.Payload);

            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful
            Assert.Equal(numberOfGuests,
                result.Payload._MaxNumberOfGuests.Value); // Ensure max number of guests is set correctly
        }
    }

    public class S2
    {
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithDraftOrReadyStatusAndValidNumberOfGuests_Success(
            int numberOfGuests)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();
            var maxNoOfGuest = MaxNumberOfGuests.Create(numberOfGuests);

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(maxNoOfGuest.Payload);

            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful
            Assert.Equal(numberOfGuests,
                result.Payload._MaxNumberOfGuests.Value); // Ensure max number of guests is set correctly
        }
    }


    public class S3
    {
        [Theory]
        [InlineData(5)]
        [InlineData(25)]
        [InlineData(50)]
        public void SetMaxNumberOfGuests_ExistingEventWithActiveStatusAndValidNumberOfGuests_Success(int numberOfGuests)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent(); // Create event with active status
            var currentMaxNumberOfGuests = viaEvent._MaxNumberOfGuests.Value; // Store current max number of guests
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(numberOfGuests); // Create new max number of guests
            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.True(result.IsSuccess); // Check if the operation was successful
            Assert.Equal(numberOfGuests,
                result.Payload._MaxNumberOfGuests.Value); // Ensure max number of guests is set correctly
            Assert.True(numberOfGuests >=
                        currentMaxNumberOfGuests); // Ensure the new value is greater than or equal to the previous value
        }
    }
    
    public class F1
    {
        [Fact]
        public void SetMaxNumberOfGuests_ReduceNumberOfMaximumGuestsInActiveEvent_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent();
            var currentMaxNumberOfGuests = viaEvent._MaxNumberOfGuests.Value; // Store current max number of guests
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(currentMaxNumberOfGuests - 1); // Try to reduce the max number of guests by 1

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal("Maximum number of guests cannot be reduced in an active event", result.Error.Messages[0].ToString()); // Check if the failure message is correct
        }
    }

    public class F2
    {
        [Fact]
        public void SetMaxNumberOfGuests_CancelledEvent_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent(); // Create event with cancelled status
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(10); // Set the new maximum number of guests

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal("Cancelled event cannot be modified", result.Error.Messages[0].ToString());
        }
    }

    
    
    public class F4
    {
        [Fact]
        public void SetMaxNumberOfGuests_LessThan5_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(4); // Set the new maximum number of guests less than 5

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal("Maximum number of Guests cannot be less than 5 or more than 50 ", result.Error.Messages[0].ToString()); // Check if the failure message is correct
        }
    }

    public class F5
    {
        [Fact]
        public void SetMaxNumberOfGuests_MoreThan50_Failure()
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateEvent();
            var newMaxNumberOfGuests = MaxNumberOfGuests.Create(51); // Set the new maximum number of guests more than 50

            // Act
            var result = viaEvent.SetMaxNumberOfGuests(newMaxNumberOfGuests.Payload);

            // Assert
            Assert.False(result.IsSuccess); // Check if the operation failed
            Assert.Equal("Maximum number of Guests cannot be less than 5 or more than 50 ", result.Error.Messages[0].ToString()); // Check if the failure message is correct
        }
    }

    
}