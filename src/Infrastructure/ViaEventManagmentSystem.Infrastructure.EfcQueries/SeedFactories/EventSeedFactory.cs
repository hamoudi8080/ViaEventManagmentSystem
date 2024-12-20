using System.Text.Json;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries.SeedFactories;


public class EventSeedFactory
{
  
    public static  List<ViaEvent> Seed()
    {
        var projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        var path = Path.Combine(projectRoot, @"src\Infrastructure\ViaEventManagmentSystem.Infrastructure.EfcQueries\JsonFiles\Events.json");
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