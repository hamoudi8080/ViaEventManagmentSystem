using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventTest
{
    [Fact]
    public void Create_ValidEvent_ReturnsViaEvent()
    {
        
        // Arrange
        Result<EventTitle> titleResult = EventTitle.Create("Via Event");
        Result<EventDescription> descriptionResult = EventDescription.Create("Test Description");
        Result<MaxNumberOfGuests> maxNumberOfGuestsResult = MaxNumberOfGuests.Create(100);
        Result<EventVisibility> eventVisibilityResult = Result<EventVisibility>.Success(EventVisibility.Public);
        EventStatus eventStatus = EventStatus.Draft;
        

// Act
        var startDateTime = StartDateTime.Create(DateTime.Now).Payload;
        var endDateTime = EndDateTime.Create(startDateTime.Value.AddHours(2)).Payload;
        var id = EventId.Create();
        var result = ViaEvent.Create(id.Payload, titleResult.Payload, descriptionResult.Payload, startDateTime, endDateTime, maxNumberOfGuestsResult.Payload, eventVisibilityResult.Payload, eventStatus);

// Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Payload);
        Assert.Equal(titleResult.Payload, result.Payload._EventTitle);
        Assert.Equal(descriptionResult.Payload, result.Payload._Description);
        Assert.Equal(startDateTime, result.Payload._StartDateTime);
        Assert.Equal(endDateTime, result.Payload._EndDateTime);
        Assert.Equal(maxNumberOfGuestsResult.Payload, result.Payload._MaxNumberOfGuests);
        Assert.Equal(eventVisibilityResult.Payload, result.Payload._EventVisibility);
        Assert.Equal(eventStatus, result.Payload._EventStatus);

        
        
        
        
        

      
    }


    [Fact]
    public void Create_Event_ReturnsViaEvent2()
    {
        // Arrange
      
        // Act
        var id = EventId.Create();
        var result2 = ViaEvent.Create(id.Payload);

        // Assert
        Assert.True(result2.IsSuccess);
        var viaEvent = result2.Payload;
       
        Assert.Equal(EventStatus.Draft, viaEvent._EventStatus);
        Assert.Equal(5, viaEvent._MaxNumberOfGuests.Value);
        Assert.Equal("Working Title", viaEvent._EventTitle?.Value);
    }
}