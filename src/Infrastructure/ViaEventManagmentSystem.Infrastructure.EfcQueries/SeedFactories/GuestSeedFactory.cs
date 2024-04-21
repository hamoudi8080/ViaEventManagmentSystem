using System.Text.Json;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries.SeedFactories;

public class GuestSeedFactory
{
    public static List<Guest> Seed()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), @"JsonFiles\Guests.json");
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