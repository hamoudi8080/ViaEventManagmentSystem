using Microsoft.EntityFrameworkCore;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace VieEventAssociation.Infrastructure.SqliteDmPersistence;

public class SqliteDmContext(DbContextOptions options) : DbContext(options)
{
    
    //The OnModelCreating method finds all EntityConfiguration classes
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventEntityConfiguration).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
    }
    public DbSet<ViaEvent> Events => Set<ViaEvent>();
    public DbSet<ViaGuest> Guests => Set<ViaGuest>();
}