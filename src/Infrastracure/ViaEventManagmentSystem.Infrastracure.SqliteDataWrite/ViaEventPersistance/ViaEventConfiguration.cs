using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects.Util;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.ViaGuest;
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
       
        // Now we configure the join table
    

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
        
        
        
        
        
        // Conversion for StartDateTime
        var startDateTimeConverter = new ValueConverter<StartDateTime, DateTime>(
            v => v.Value,
            v => StartDateTime.Create(v).Payload);
        builder
            .Property(e => e._StartDateTime)
            .HasConversion(startDateTimeConverter);

        // Conversion for EndDateTime
        var endDateTimeConverter = new ValueConverter<EndDateTime, DateTime>(
            v => v.Value,
            v => EndDateTime.Create(v).Payload);
        builder
            .Property(e => e._EndDateTime)
            .HasConversion(endDateTimeConverter);

        // Conversion for MaxNumberOfGuests
        var maxNumberOfGuestsConverter = new ValueConverter<MaxNumberOfGuests, int>(
            v => v.Value,
            v => MaxNumberOfGuests.Create(v).Payload);
        builder
            .Property(e => e._MaxNumberOfGuests)
            .HasConversion(maxNumberOfGuestsConverter);
 
        
        /*
        // Conversion for EventVisibility
        var eventVisibilityConverter = new ValueConverter<EventVisibility, string>(
            v => v.ToString(),
            v => (EventVisibility)Enum.Parse(typeof(EventVisibility), v));
        builder
            .Property(e => e._EventVisibility)
            .HasConversion(eventVisibilityConverter);
            */
        var eventVisibilityConverter = new ValueConverter<EventVisibility, string>(
            v => v.Value.ToString(),
            v => EventVisibility.From((EventVisibilityEnum)Enum.Parse(typeof(EventVisibilityEnum), v)));
        builder
            .Property(e => e._EventVisibility)
            .HasConversion(eventVisibilityConverter);

// Conversion for EventStatus
        var eventStatusConverter = new ValueConverter<EventStatus, string>(
            v => v.ToString(),
            v => (EventStatus)Enum.Parse(typeof(EventStatus), v));
        builder
            .Property(e => e._EventStatus)
            .HasConversion(eventStatusConverter);
    
        
    }
}