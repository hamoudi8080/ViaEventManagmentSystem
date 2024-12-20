using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
 

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.GuestPersistance;

public class GuestConfiguration : IEntityTypeConfiguration<ViaGuest>
{
    public void Configure(EntityTypeBuilder<ViaGuest> builder)
    {
        builder.HasKey(e => e.Id);

        // Configure GuestId
        var guestIdConverter = new ValueConverter<GuestId, Guid>(
            v => v.Value,
            v => GuestId.Create(v.ToString()).Payload);
        builder.Property(e => e.Id).HasConversion(guestIdConverter);

        // Configure FirstName
        var firstNameConverter = new ValueConverter<FirstName, string>(
            v => v.Value,
            v => FirstName.Create(v.ToString()).Payload);
        builder.Property(e => e._FirstName).HasConversion(firstNameConverter);

        // Configure LastName
        var lastNameConverter = new ValueConverter<LastName, string>(
            v => v.Value,
            v => LastName.Create(v.ToString()).Payload);
        builder.Property(e => e._LastName).HasConversion(lastNameConverter);

        // Configure Email
        var emailConverter = new ValueConverter<Email, string>(
            v => v.Value,
            v => Email.Create(v.ToString()).Payload);
        builder.Property(e => e._Email).HasConversion(emailConverter);
    }


}



 