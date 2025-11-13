using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace VieEventAssociation.Infrastructure.SqliteDmPersistence;

public class InvitationEntityConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable("Invitations");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(
                id => id.Value,
                value => InvitationId.Create(value.ToString()).Payload);

        builder.Property<InvitationStatus>("_InvitationStatus")
            .HasColumnName("Status")
            .HasConversion(
                v => v.ToString(),
                v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v));

        // Configure the backing field for EventId
        builder.Property<EventId>("_EventId")
            .HasColumnName("EventId")
            .HasConversion(id => id.Value,
                value => EventId.Create(value.ToString()).Payload);

        // Configure the backing field for GuestId
        builder.Property<GuestId>("_GuestId")
            .HasColumnName("GuestId")
            .HasConversion(id => id.Value,
                value => GuestId.Create(value.ToString()).Payload);
        // ⭐ NEW: Add relationship to Guest
        // This ensures referential integrity - invitations must reference valid guests
    
        builder.HasOne<ViaGuest>()
            .WithMany()
            .HasForeignKey("_GuestId")  // Column name (string), not backing field
            .OnDelete(DeleteBehavior.Restrict) // Prevent deleting guests with pending invitations
            .IsRequired();
    }
}