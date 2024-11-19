using UnitTests.Common.Factories.EventFactory;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.EventTimeDurationTest;

public abstract class EventTimeDurationTest
{
    public class S1
    {
        [Theory]
        [InlineData("2026/08/25 19:00", "2026/08/25 23:59")]
        [InlineData("2026/08/25 12:00", "2026/08/25 16:30")]
        [InlineData("2026/08/25 08:00", "2026/08/25 12:15")]
        [InlineData("2026/08/25 10:00", "2026/08/25 20:00")]
        [InlineData("2026/08/25 13:00", "2026/08/25 23:00")]
        public void UpdateEventTimes_ValidInput_Success(string startTimeString, string endTimeString)
        {
            // Arrange
            var validStartDateTime = DateTime.Parse(startTimeString);
            var validEndDateTime = DateTime.Parse(endTimeString);
            var viaEvent = ViaEventTestFactory.DraftEvent();

            var _startDateTime = StartDateTime.Create(validStartDateTime).Payload;
            var _endDateTime = EndDateTime.Create(validEndDateTime).Payload;

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(validStartDateTime, viaEvent._StartDateTime.Value);
            Assert.Equal(validEndDateTime, viaEvent._EndDateTime.Value);
        }
    }


    public class S2
    {
        [Theory]
        [InlineData("2026/08/25 19:00", "2026/08/26 01:00")]
        [InlineData("2026/08/25 12:00", "2026/08/25 16:30")]
        [InlineData("2026/08/25 08:00", "2026/08/25 12:15")]
        public void SetEventTimes_ValidInput_Success(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange

            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var createEvent = ViaEventTestFactory.DraftEvent();

            // Act
            var result = createEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }


    public class S3
    {
        [Theory]
        [InlineData("2026/08/25 19:00", "2026/08/26 01:00")]
        // You can add more test data here as per S1 and S2
        public void SetEventTimes_ValidInput_StatusUpdatedToDraft(DateTime startDateTimeString, DateTime endDateTimeString)
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
        [InlineData("2026/08/25 19:00", "2026/08/26 01:00")] // Future start time
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
        [InlineData("2026/08/25 08:00", "2026/08/25 18:00")] 
        public void SetEventTimes_ValidInput_DurationTenHoursOrLess(DateTime startDateTimeString,
            DateTime endDateTimeString)
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

    public class F1
    {
        [Theory]
        [InlineData("2023/08/26 19:00", "2023/08/25 01:00")] // Start date after end date
        [InlineData("2023/08/26 19:00", "2023/08/25 23:59")] // Start date after end date
        [InlineData("2023/08/27 12:00", "2023/08/25 16:30")] // Start date after end date
        [InlineData("2023/08/01 08:00", "2023/07/31 12:15")] // Start date after end date
        public void SetEventTimes_InvalidInput_StartDateAfterEndDate(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); 

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.StartTimeMustBeBeforeEndTime.DisplayName, result.Error.Messages[0].ToString());
        }
    }

    public class F2
    {
        [Theory]
        [InlineData("2026/08/26 19:00", "2026/08/26 14:00")] // Same start and end date, start time after end time
        [InlineData("2026/08/26 16:00", "2026/08/26 00:00")] // Same start and end date, start time after end time
        [InlineData("2026/08/26 19:00", "2026/08/26 18:59")] // Same start and end date, start time after end time
        [InlineData("2026/08/26 12:00", "2026/08/26 10:10")] // Same start and end date, start time after end time
        [InlineData("2026/08/26 08:00", "2026/08/26 00:30")] // Same start and end date, start time after end time
        public void SetEventTimes_InvalidInput_StartTimeAfterEndTime(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Assuming factory method to create event

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.StartTimeMustBeBeforeEndTime.DisplayName, result.Error.Messages[0].DisplayName);
        }
    }

    public class F3
    {
        [Theory]
        
        [InlineData("2026/08/26 08:00", "2026/08/26 08:00")] // Same start and end date, duration less than 1 hour
        public void SetEventTimes_InvalidInput_DurationLessThanOneHour(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Assuming factory method to create event

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventDurationLessThan1Hour.DisplayName, result.Error.Messages[0].ToString());
        }
    }


    public class F4
    {
        [Theory]
        [InlineData("2026/08/25 23:30", "2026/08/26 00:15")] // Start date before end date, duration less than 1 hour
        [InlineData("2026/08/30 23:01", "2026/08/31 00:00")] // Start date before end date, duration less than 1 hour
        [InlineData("2026/08/30 23:59", "2026/08/31 00:01")] // Start date before end date, duration less than 1 hour
        public void SetEventTimes_InvalidInput_StartBeforeEndAndDurationLessThanOneHour(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Assuming factory method to create event

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventDurationLessThan1Hour.DisplayName, result.Error.Messages[0].ToString());
        }
    }

    public class F5
    {
        [Theory]
        [InlineData("2023/08/25 07:50", "2023/08/25 14:00")] // Start time before 08:00
        [InlineData("2023/08/25 07:59", "2023/08/25 15:00")] // Start time before 08:00
        [InlineData("2023/08/25 01:01", "2023/08/25 08:30")] // Start time before 08:00
        [InlineData("2023/08/25 05:59", "2023/08/25 07:59")] // Start time before 08:00
        [InlineData("2023/08/25 00:59", "2023/08/25 07:59")] // Start time before 08:00
        public void SetEventTimes_InvalidInput_StartTimeBefore0800(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent();

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventCannotStartBefore8Am.DisplayName, result.Error.Messages[0].ToString());
        }
    }

    /*
    public class F6
    {
        [Theory]
        [InlineData("2026/08/24 23:50", "2026/08/25 01:01")] // Start time before 01:00, end time after 01:00
        [InlineData("2026/08/24 22:00", "2026/08/25 07:59")] // Start time before 01:00, end time after 01:00
        [InlineData("2026/08/30 23:00", "2026/08/31 02:30")] // Start time before 01:00, end time after 01:00
        [InlineData("2026/08/24 23:50", "2026/08/25 01:01")] // Start time before 01:00, end time after 01:00
        public void SetEventTimes_InvalidInput_StartBefore01EndAfter01(DateTime startDateTimeString,
            DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent();  

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.StartTimeBefore1EndTimeAfter1.DisplayName, result.Error.Messages[0].ToString());
        }
    }
    */
    
    
    
    public class F7
    {
        [Theory]
        [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
        [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
        [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
        [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
        [InlineData("2023/08/25 08:00", "2023/08/25 23:00")]
        public void SetEventTimes_Failure_ActiveEvent(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CreateActiveEvent();
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.ActiveEventCanotBeModified.DisplayName, result.Error.Messages[0].ToString());
        }
    }

    
    public class F8
    {
        [Theory]
        [InlineData("2023/08/25 19:00", "2023/08/25 23:59")]
        [InlineData("2023/08/25 12:00", "2023/08/25 16:30")]
        [InlineData("2023/08/25 08:00", "2023/08/25 12:15")]
        [InlineData("2023/08/25 10:00", "2023/08/25 20:00")]
        [InlineData("2023/08/25 08:00", "2023/08/25 23:00")]
        public void SetEventTimes_Failure_CancelledEvent(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange
            var viaEvent = ViaEventTestFactory.CancelledEvent();
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.CancelledEventCannotBemodified.DisplayName, result.Error.Messages[0].ToString());
        }
    }
    
    
    public class F9
    {
        [Theory]
        [InlineData("2026/08/30 08:00", "2026/08/30 18:01")] // Duration longer than 10 hours
        [InlineData("2026/08/30 14:59", "2026/08/31 01:00")] // Duration longer than 10 hours
        [InlineData("2026/08/30 14:00", "2026/08/31 00:01")] // Duration longer than 10 hours
        [InlineData("2026/08/30 14:00", "2026/08/31 18:30")] // Duration longer than 10 hours
        public void SetEventTimes_Failure_LongerThan10Hours(DateTime startDateTimeString, DateTime endDateTimeString)
        {
            // Arrange
            var _startDateTime = StartDateTime.Create(startDateTimeString).Payload;
            var _endDateTime = EndDateTime.Create(endDateTimeString).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); // Assuming factory method to create event

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventDurationGreaterThan10Hours.DisplayName, result.Error.Messages[0].ToString());
        }
    }

    
    public class F10
    {
        [Fact]
        public void SetEventTimes_Failure_StartTimeInThePast()
        {
            // Arrange
            var pastStartDateTime = DateTime.Now.AddHours(-1); // Assume the start time is one hour ago
            var futureEndDateTime = DateTime.Now.AddHours(1); // Assume the end time is one hour from now
            var _startDateTime = StartDateTime.Create(pastStartDateTime).Payload;
            var _endDateTime = EndDateTime.Create(futureEndDateTime).Payload;
            var viaEvent = ViaEventTestFactory.CreateEvent(); 

            // Act
            var result = viaEvent.EventTimeDuration(_startDateTime, _endDateTime);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ErrorMessage.EventStartTimeCannotBeInPast.DisplayName, result.Error.Messages[0].ToString());
        }
    }


}