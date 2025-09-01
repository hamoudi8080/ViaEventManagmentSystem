using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.InvitationPersistance;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);

        // Configure InvitationId
        var invitationIdConverter = new ValueConverter<InvitationId, Guid>(
            v => v.Value,
            v => InvitationId.Create(v.ToString()).Payload);
        builder.Property(i => i.Id).HasConversion(invitationIdConverter);

        // Configure EventId
        var eventIdConverter = new ValueConverter<EventId, Guid>(
            v => v.Value,
            v => EventId.Create(v.ToString()).Payload);
        builder.Property(e => e._EventId).HasConversion(eventIdConverter);

        // Configure GuestId
        var guestIdConverter = new ValueConverter<GuestId, Guid>(
            v => v.Value,
            v => GuestId.Create(v.ToString()).Payload);
        builder.Property(e => e._GuestId).HasConversion(guestIdConverter);

        // Configure InvitationStatus
        builder.Property(e => e._InvitationStatus)
            .HasConversion(
                v => v.Value,
                v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v.ToString()));
    }
    /*
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);

        var invitationIdConverter = new ValueConverter<InvitationId, Guid>(
            v => v.Value,
            v => InvitationId.Create(v.ToString()).Payload);

        builder
            .Property(i => i.Id)
            .HasConversion(invitationIdConverter);
        
        
        var eventIdConverter = new ValueConverter<EventId, Guid>(
            v => v.Value,
            v => EventId.Create(v.ToString()).Payload);

        builder
            .Property(e => e._EventId) // Assuming EventId is a property of Invitation
            .HasConversion(eventIdConverter);

        var guestIdConverter = new ValueConverter<GuestId, Guid>(
            v => v.Value,
            v => GuestId.Create(v.ToString()).Payload);

        builder
            .Property(e => e._GuestId) // Assuming GuestId is a property of Invitation
            .HasConversion(guestIdConverter);




        builder
            .Property(e => e._InvitationStatus)
            .HasConversion(
                v => v.Value,
                v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v.ToString())
            );


        // Other configuration...
        // TODO configure relationship to guest. Guest Id must be foreign key to a guest
    }
    */
}