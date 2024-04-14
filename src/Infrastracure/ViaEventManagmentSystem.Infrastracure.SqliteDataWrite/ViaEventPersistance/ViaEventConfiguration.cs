using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.ViaEventPersistance;

public class ViaEventConfiguration : IEntityTypeConfiguration<ViaEvent>
{
    public void Configure(EntityTypeBuilder<ViaEvent> builder)
    {
     
        
        builder.HasKey(e => e.Id);

        var eventIdConverter = new ValueConverter<EventId, Guid>(
            v => v.Value,
            v => EventId.Create(v.ToString()).Payload);

        builder
            .Property(e => e.Id)
            .HasConversion(eventIdConverter);
       
        
        /*
        builder.HasKey(e => e._eventId);
        builder
            .Property(e => e._eventId)
            .HasConversion(eventId => eventId.Value, value => EventId.Create(value.ToString()).Payload);
        */
        
        var eventTitleConverter = new ValueConverter<EventTitle, string>(
            v => v.Value,
            v => EventTitle.Create(v).Payload);

        builder
            .Property(e => e._EventTitle)
            .HasConversion(eventTitleConverter);
        

        var eventDescriptionConverter = new ValueConverter<EventDescription, string>(
            v => v.Value,
            v => EventDescription.Create(v).Payload);

        builder
            .Property(e => e._Description)
            .HasConversion(eventDescriptionConverter);
        

 


        builder
            .HasMany(e => e._Invitations)
            .WithOne() // Assuming there is no navigation property in the Invitation class
            .HasForeignKey("_EventId"); // Assuming the foreign key in the Invitation class is _EventId
    
        
        
        /*
        
        builder
            .HasMany(e => e._GuestsParticipants)
            .WithOne() // Assuming there is no navigation property in the Guest class
            .HasForeignKey("_GuestId"); // Assuming the foreign key in the Guest class is _GuestId
        
        
        */
        
        builder
            .HasMany<Guest>("_GuestsParticipants") // Assuming Guest is the entity related to GuestId
            .WithOne("ViaEvent") // Assuming ViaEvent is a navigation property in the Guest entity
            .HasForeignKey("Id"); // Assuming the foreign key in the Guest entity is _GuestId
        /*
        // Configure relationships
        builder
            .HasMany(e => e._GuestsParticipants)
            .WithOne() // No navigation property in the GuestId class
            .HasForeignKey("_EventId"); // Assuming the foreign key in the Invitation class is _EventId
            ;
            */
    }
}