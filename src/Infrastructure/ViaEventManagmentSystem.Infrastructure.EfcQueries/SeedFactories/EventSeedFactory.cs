using System.Text.Json;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries.SeedFactories;


public class EventSeedFactory
{
    public static  List<ViaEvent> Seed()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), @"JsonFiles\Events.json");
        var jsonData = File.ReadAllText(path);
        var eventsData = JsonSerializer.Deserialize<List<TmpEvent>>(jsonData);

        var events = eventsData.Select(e => new ViaEvent
        {
            Id = e.Id,
            EventTitle = e.Title,
            Description = e.Description,
            StartDateTime = e.Start,
            EndDateTime = e.End,
            MaxNumberOfGuests = e.MaxGuests,
            EventVisibility = e.Visibility,
            EventStatus = e.Status
        }).ToList();

        return events;
    }

    public record TmpEvent(
        string Id,
        string Title,
        string Description,
        string Status,
        string Visibility,
        string Start,
        string End,
        int MaxGuests);
}