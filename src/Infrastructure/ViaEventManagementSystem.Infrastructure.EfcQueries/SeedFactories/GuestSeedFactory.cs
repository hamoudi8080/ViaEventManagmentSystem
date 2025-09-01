using System.Text.Json;

namespace ViaEventManagementSystem.Infrastructure.EfcQueries.SeedFactories;

public class GuestSeedFactory
{
 
    public static List<Guest> Seed()
    {
        // var projectRoot = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        // var path = Path.Combine(projectRoot, @"ViaEventManagmentSystem\src\Infrastructure\ViaEventManagementSystem.Infrastructure.EfcQueries\JsonFiles\Guests.json");
        var projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        var path = Path.Combine(projectRoot, @"src\Infrastructure\ViaEventManagementSystem.Infrastructure.EfcQueries\JsonFiles\Guests.json");
        var jsonData = File.ReadAllText(path);
        var guestsData = JsonSerializer.Deserialize<List<TmpGuest>>(jsonData);
        var guests = guestsData.Select(g => new Guest
        {
            Id = g.Id,
            FirstName = g.FirstName,
            LastName = g.LastName,
            Email = g.Email
        }).ToList();

        return guests;
    }


    public record TmpGuest(
        string Id,
        string FirstName,
        string LastName,
        string Email
    );
     
}