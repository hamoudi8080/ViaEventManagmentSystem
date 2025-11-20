using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

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
            int maxNumberOfGuests = 5;
            string eventDescription = "Testing Event";

            // Act
            var result = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime, maxNumberOfGuests, eventDescription);
         
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payload);

            var cmd = result.Payload!;
            Assert.Equal(id.Payload.Value, cmd.EventId.Value);
            Assert.Equal(eventTitle, cmd.Title.Value);
            Assert.Equal(eventDescription, cmd.Description.Value);
            Assert.Equal(startDateTime, cmd.Start.Value);
            Assert.Equal(endDateTime, cmd.End.Value);
            Assert.Equal(maxNumberOfGuests, cmd.MaxGuests.Value);
            
        
        }
        
        
        [Fact]
        public void Create_ShouldReturnFalling_WhenInValidParametersAreProvided()
        {
            // Arrange
            var eid = Guid.NewGuid();
            var id = EventId.Create(eid.ToString());
            string eventTitle = null;
            DateTime startDateTime = DateTime.Now.AddDays(1);
            DateTime endDateTime = startDateTime.AddHours(2);
            int maxNumberOfGuests = 2;
            string eventDescription = "Testing Event";

            // Act
            var result = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime, maxNumberOfGuests, eventDescription);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorCollection);
            Assert.NotEmpty(result.ErrorCollection);
            
            
        }
    }
}