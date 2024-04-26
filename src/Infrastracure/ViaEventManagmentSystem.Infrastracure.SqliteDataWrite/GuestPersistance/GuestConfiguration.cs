using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
 

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
        
        
        
        
        var firstNameConverter = new ValueConverter<FirstName, string>(
            v => v.Value,
            v => FirstName.Create(v.ToString()).Payload);


        builder
            .Property(e => e._FirstName)
            .HasConversion(firstNameConverter);
        
        var lastNameConverter = new ValueConverter<LastName, string>(
            v => v.Value,
            v => LastName.Create(v.ToString()).Payload);


        builder
            .Property(e => e._LastName)
            .HasConversion(lastNameConverter);
        
        var emailConverter = new ValueConverter<Email, string>(
            v => v.Value,
            v => Email.Create(v.ToString()).Payload);


        builder
            .Property(e => e._Email)
            .HasConversion(emailConverter);

       
        
 
            
    }

}



 