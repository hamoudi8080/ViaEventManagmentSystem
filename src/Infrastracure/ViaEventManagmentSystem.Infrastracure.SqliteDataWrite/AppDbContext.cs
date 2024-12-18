﻿using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
 
  
    /*
    public DbSet<Guest> Guests => Set<Guest>();
     // It means that ViaEvents can only be read from, not written to. This is not a common way to define DbSets,
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<ViaEvent> ViaEvents => Set<ViaEvent>();

  */
    public DbSet<ViaGuest> Guests { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<ViaEvent> ViaEvents { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // First Ids on both
        modelBuilder.Entity<ViaEvent>().HasKey(x => x.Id);
        modelBuilder.Entity<ViaGuest>().HasKey(x => x.Id);


        // Then the conversion from strong ID to simple type
        modelBuilder.Entity<ViaGuest>() // here we define the conversion for the ID
            .Property(m => m.Id)
            .HasConversion(
                id => id.Value, // how to convert ID type to simple value, EFC can understand
                value => GuestId.Create(value.ToString()).Payload); // how to convert simple EFC value to strong ID.
        
        // modelBuilder.Entity<GuestParticipation>(x =>
        // {
        //     x.Property<EventId>("EventId");
        //     x.Property<GuestId>("GuestId");
        //     x.HasKey("EventId", "GuestId");
        //     x.HasOne<ViaEvent>()
        //         .WithMany("_GuestsParticipants")
        //         .HasForeignKey("EventId");
        //
        //     x.Property(m => m.GuestId)
        //         .HasConversion(
        //             id => id.Value, // how to convert ID type to simple value, EFC can understand
        //             value => GuestId.Create(value.ToString()).Payload); // how to convert simple EFC value to strong ID.
        //
        //     x.HasOne<Guest>()
        //         .WithMany()
        //         .HasForeignKey(y => y.GuestId);
        // });
        //
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
      
      
    }
}



//modelBuilder.Entity<GuestId>().HasKey(v => v.Value);