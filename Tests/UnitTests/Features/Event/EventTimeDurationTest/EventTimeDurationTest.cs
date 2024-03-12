using UnitTests.Features.Event.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.EventTimeDurationTest;

public abstract class EventTimeDurationTest
{
    public class S1
    {
        [Theory]
        [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
        [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
        [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
        [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
        [InlineData("2023/08/25 08:00", "2023/08/25 23:00")]
        public void UpdateEventTimes_ValidInput_Success(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange


            var createEvent = ViaEventTestFactory.CreateEvent();

            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;

            // Act
            var result = createEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(startDateTimeString, createEvent._StartDateTime.Value);
            Assert.Equal(endDateTimeString, createEvent._EndDateTime.Value);
        }
    }


    public class S2
    {
        [Theory]
        [InlineData("2023/08/25 19:00", "2023/08/26 01:00")]
        [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
        [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
        public void SetEventTimes_ValidInput_Success(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange

            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var createEvent = ViaEventTestFactory.CreateEvent();

            // Act
            var result = createEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }


    public class S3
    {
        [Theory]
        [InlineData("2023/08/25 19:00", "2023/08/26 01:00")]
        // You can add more test data here as per S1 and S2
        public void SetEventTimes_ValidInput_StatusUpdatedToDraft(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;

            var viaEvent = ViaEventTestFactory.ReadyEvent();

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
            Assert.Equal(_startDateTime, viaEvent._StartDateTime);
            Assert.Equal(_endDateTime, viaEvent._EndDateTime);
        }
    }

    public class S4
    {
        [Theory]
        [InlineData("2024/08/25 19:00", "2024/08/26 01:00")] // Future start time
 
        public void SetEventTimes_ValidInput_FutureStartTime(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); 

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_startDateTime, viaEvent._StartDateTime);
            Assert.Equal(_endDateTime, viaEvent._EndDateTime);
        }
    }
    
    
    
    public class S5
    {
        [Theory]
        [InlineData("2023/08/25 08:00", "2023/08/25 18:00")] // 10-hour duration
     
        public void SetEventTimes_ValidInput_DurationTenHoursOrLess(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Assuming factory method to create event

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(_startDateTime, viaEvent._StartDateTime);
            Assert.Equal(_endDateTime, viaEvent._EndDateTime);
        }
    }
}