using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSystem.Infrastructure.EfcQueries.SeedFactories;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries;

public partial class VeadatabaseProductionContext : DbContext
{
    public VeadatabaseProductionContext()
    {
    }

    public VeadatabaseProductionContext(DbContextOptions<VeadatabaseProductionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<ViaEvent> ViaEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite(
            "Data Source=C:\\Users\\hamod\\Desktop\\New folder (2)\\ViaEventManagmentSystem\\src\\Infrastracure\\ViaEventManagmentSystem.Infrastracure.SqliteDataWrite\\VEADatabaseProduction.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.Email).HasColumnName("_Email");
            entity.Property(e => e.FirstName).HasColumnName("_FirstName");
            entity.Property(e => e.LastName).HasColumnName("_LastName");
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasIndex(e => e.EventId, "IX_Invitations__EventId");

            entity.Property(e => e.EventId).HasColumnName("_EventId");
            entity.Property(e => e.GuestId).HasColumnName("_GuestId");
            entity.Property(e => e.InvitationStatus).HasColumnName("_InvitationStatus");

            entity.HasOne(d => d.Event).WithMany(p => p.Invitations).HasForeignKey(d => d.EventId);
        });

        modelBuilder.Entity<ViaEvent>(entity =>
        {
            entity.Property(e => e.Description).HasColumnName("_Description");
            entity.Property(e => e.EndDateTime).HasColumnName("_EndDateTime");
            entity.Property(e => e.EventStatus).HasColumnName("_EventStatus");
            entity.Property(e => e.EventTitle).HasColumnName("_EventTitle");
            entity.Property(e => e.EventVisibility).HasColumnName("_EventVisibility");
            entity.Property(e => e.MaxNumberOfGuests).HasColumnName("_MaxNumberOfGuests");
            entity.Property(e => e.StartDateTime).HasColumnName("_StartDateTime");

            entity.HasMany(d => d.Guests).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "GuestParticipation",
                    r => r.HasOne<Guest>().WithMany().HasForeignKey("GuestId"),
                    l => l.HasOne<ViaEvent>().WithMany().HasForeignKey("EventId"),
                    j =>
                    {
                        j.HasKey("EventId", "GuestId");
                        j.ToTable("GuestParticipation");
                        j.HasIndex(new[] { "GuestId" }, "IX_GuestParticipation_GuestId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public static VeadatabaseProductionContext Seed(VeadatabaseProductionContext context)
    {
        context.Guests.AddRange(GuestSeedFactory.Seed());
        List<ViaEvent> veaEvents = EventSeedFactory.Seed();
        context.ViaEvents.AddRange(veaEvents);
        context.SaveChangesAsync();
        //ParticipationSeedFactory.Seed(context);
        //context.SaveChangesAsync();
       // InvitationSeedFactory.Seed(context);
        context.SaveChangesAsync();
        return context;
    }

    public static VeadatabaseProductionContext SetupReadContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<VeadatabaseProductionContext>();

        string testDbName = "Test" + Guid.NewGuid() + ".db";

        optionsBuilder.UseSqlite("Data Source=" + testDbName);
        VeadatabaseProductionContext context = new(optionsBuilder.Options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
}