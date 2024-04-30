using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Features.Event.CreateEvent
{
    public class CreateEventCommandTest
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenValidParametersAreProvided()
        {
            // Arrange
            var eid = Guid.NewGuid();
            var id = EventId.Create(eid.ToString());
            string eventTitle = "Test Event";
            DateTime startDateTime = DateTime.Now.AddDays(1);
            DateTime endDateTime = startDateTime.AddHours(2);
            int maxNumberOfGuests = 33;
            string eventDescription = "Test Event Description";

            // Act
            var result = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime, maxNumberOfGuests, eventDescription);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payload);
           
            Assert.Equal(eventTitle, result.Payload.ViaEvent._EventTitle.Value);
            Assert.Equal(startDateTime, result.Payload.ViaEvent._StartDateTime.Value);
            Assert.Equal(endDateTime, result.Payload.ViaEvent._EndDateTime.Value);
            Assert.Equal(maxNumberOfGuests, result.Payload.ViaEvent._MaxNumberOfGuests.Value);
            Assert.Equal(eventDescription, result.Payload.ViaEvent._Description.Value);
        }
    }
}