using System.Text.Json;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

namespace ViaEventManagementSystem.Infrastructure.EfcQueries.SeedFactories;


public class EventSeedFactory
{
  
    public static  List<ViaEvent> Seed()
    {
        var projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        var path = Path.Combine(projectRoot, @"src\Infrastructure\ViaEventManagementSystem.Infrastructure.EfcQueries\JsonFiles\Events.json");
        var jsonData = File.ReadAllText(path);
        var eventsData = JsonSerializer.Deserialize<List<TmpEvent>>(jsonData);

        var events = eventsData.Select(e => new ViaEvent
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Status = Enum.Parse<EventStatus>(e.Status),
            Visibility = Enum.Parse<EventVisibility>(e.Visibility),
            Start = DateTime.Parse(e.Start),
            End = DateTime.Parse(e.End),
            MaxGuests = e.MaxGuests
    
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