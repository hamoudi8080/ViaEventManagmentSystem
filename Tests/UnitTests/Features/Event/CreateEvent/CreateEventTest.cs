using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventTest
{
    [Fact]
    public void Create_ValidEvent_ReturnsViaEvent()
    {
        // Arrange
        var titleResult = EventTitle.Create("Via Event");
        var descriptionResult = EventDescription.Create("Test Description");
        var maxNumberOfGuestsResult = MaxNumberOfGuests.Create(100);
        var eventVisibilityResult = Result<EventVisibility>.Success(EventVisibility.Public);
        var eventStatus = EventStatus.Draft;


        // Act
        var id = EventId.Create();
        var startDateTime = StartDateTime.Create(DateTime.Now).Payload;
        var endDateTime = EndDateTime.Create(startDateTime.Value.AddHours(2)).Payload;
        var result = ViaEvent.Create(id.Payload, titleResult.Payload, descriptionResult.Payload, startDateTime,
            endDateTime, maxNumberOfGuestsResult.Payload, eventVisibilityResult.Payload, eventStatus);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
    }
    
}