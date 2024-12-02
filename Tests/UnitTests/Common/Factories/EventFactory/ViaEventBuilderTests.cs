using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace UnitTests.Common.Factories.EventFactory;

public class ViaEventBuilderTests
{
    [Fact]
    public void Create_ValidTitle_ReturnsSuccessResult()
    {
        var eventId = EventId.Create();
        var title = EventTitle.Create("Annual Conference").Payload;
        var description = EventDescription.Create("A conference about technology.").Payload;
        var startDateTime = StartDateTime.Create(new DateTime(2024, 12, 1, 9, 0, 0)).Payload;
        var endDateTime = EndDateTime.Create(new DateTime(2024, 12, 1, 17, 0, 0)).Payload;
        var maxGuests = MaxNumberOfGuests.Create(25).Payload;

        var result = ViaEventBuilder
            .Create(eventId.Payload)
            .WithTitle(title)
            .WithDescription(description)
            .WithStartDateTime(startDateTime)
            .WithEndDateTime(endDateTime)
            .WithMaxNumberOfGuests(maxGuests)
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Ready)
            .Build();
        
        

        if (result.IsSuccess)
        {
            var viaEvent = result.Payload;
            Console.WriteLine($"Event created successfully with ID: {viaEvent.Id}");
        }
         

    }
}