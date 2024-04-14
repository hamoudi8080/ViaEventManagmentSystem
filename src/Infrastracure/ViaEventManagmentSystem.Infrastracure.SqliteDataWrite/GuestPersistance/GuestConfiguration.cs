using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.Utilies;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.GuestPersistance;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
   
        /*
        
        builder.HasKey(e => e.Id);
        var eventIdConverter = new ValueConverter<GuestId, Guid>(
            v => v.Value,
            v => GuestId.Create(v.ToString()).Payload);

        builder
            .Property(e => e.Id)
            .HasConversion(eventIdConverter);
         
        */
        
        
        builder.HasKey(e => e.Id);

        var guestIdConverter = new ValueConverter<GuestId, Guid>(
            v => v.Value,
            v => GuestId.Create(v.ToString()).Payload);


        builder
            .Property(e => e.Id)
            .HasConversion(guestIdConverter);
       
        
 
            
    }

}




/*
   builder.HasKey(e => e._Id);

   var eventIdConverter = new ValueConverter<GuestId, Guid>(
       v => v.Value,
       v => GuestId.Create(v.ToString()).Payload);

   builder
       .Property(e => e._Id)
       .HasConversion(eventIdConverter);
       */
         
        
/*
builder.HasKey(e => e.Id);

var guestIdConverter = new ValueConverter<GuestId, Guid>(
    v => v.Value,
    v => GuestId.Create(v.ToString()).Payload);

builder
    .Property(e => e.Id)
    .HasConversion(guestIdConverter);
*/
        
// Configure the primary key using the base class's Id property