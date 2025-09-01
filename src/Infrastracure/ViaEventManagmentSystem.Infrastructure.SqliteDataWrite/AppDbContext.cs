using Microsoft.EntityFrameworkCore;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.GuestPersistance;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.InvitationPersistance;
using ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.ViaEventPersistance;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    public DbSet<ViaGuest> Guests => Set<ViaGuest>();
    
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<ViaEvent> ViaEvents => Set<ViaEvent>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        
        
        
        
        modelBuilder.ApplyConfiguration(new ViaEventConfiguration());
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
        modelBuilder.ApplyConfiguration(new InvitationConfiguration());
        base.OnModelCreating(modelBuilder);
        
        
        
        
        
        /*
        // First Ids on both
        modelBuilder.Entity<ViaEvent>().HasKey(x => x.Id);
        modelBuilder.Entity<ViaGuest>().HasKey(x => x.Id);


        // Then the conversion from strong ID to simple type
        modelBuilder.Entity<ViaGuest>() // here we define the conversion for the ID
            .Property(m => m.Id)
            .HasConversion(
                id => id.Value, // how to convert ID type to simple value, EFC can understand
                value => GuestId.Create(value.ToString()).Payload); // how to convert simple EFC value to strong ID.
        
*/
        
      
      
    }
}

