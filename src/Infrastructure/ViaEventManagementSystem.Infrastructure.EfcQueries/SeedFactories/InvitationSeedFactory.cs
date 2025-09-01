using System.Text.Json;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;

namespace ViaEventManagementSystem.Infrastructure.EfcQueries.SeedFactories;

public class InvitationSeedFactory
{
    public static List<Invitation> Seed()
    {
        // var projectRoot = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        // var path = Path.Combine(projectRoot, @"ViaEventManagmentSystem\src\Infrastructure\ViaEventManagementSystem.Infrastructure.EfcQueries\JsonFiles\Guests.json");
        var projectRoot = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
        var path = Path.Combine(projectRoot, @"src\Infrastructure\ViaEventManagementSystem.Infrastructure.EfcQueries\JsonFiles\Invitations.json");
        var jsonData = File.ReadAllText(path);
        var invitationData = JsonSerializer.Deserialize<List<TmpInvitation>>(jsonData);
        var invitation = invitationData.Select(g => new Invitation()
        {
            Id = g.Id,
            EventId = g.EventId,
            GuestId = g.GuestId,
            InvitationStatus = g.InvitationStatus
            
            
        }).ToList();

        return invitation;
    }


    public record TmpInvitation(
        string Id,
        string EventId,
        string GuestId,
        string InvitationStatus
    );
}