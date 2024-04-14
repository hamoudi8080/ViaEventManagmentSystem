using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
 
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<ViaEvent> ViaEvents { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
      
      
    }
}



//modelBuilder.Entity<GuestId>().HasKey(v => v.Value);