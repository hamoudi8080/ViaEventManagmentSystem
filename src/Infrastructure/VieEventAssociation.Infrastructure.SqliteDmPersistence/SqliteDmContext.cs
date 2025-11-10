using Microsoft.EntityFrameworkCore;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;

namespace VieEventAssociation.Infrastructure.SqliteDmPersistence;

public class SqliteDmContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ViaEvent> Events => Set<ViaEvent>();
    public DbSet<ViaGuest> Guests => Set<ViaGuest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}