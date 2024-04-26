using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.InvitationPersistance;

public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        /*
        builder.HasKey(i => i._Id);

        var invitationIdConverter = new ValueConverter<InvitationId, Guid>(
            v => v.Value, // Convert InvitationId to Guid for storing in the database
            v => InvitationId.Create(v.ToString()).Payload); // Convert Guid to InvitationId when reading from the database

        builder
            .Property(i => i._Id)
            .HasConversion(invitationIdConverter);
*/


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
            )
            .HasColumnName("_InvitationStatus");
        
        // Other configuration...
        // TODO configure relationship to guest. Guest Id must be foreign key to a guest
    }
}