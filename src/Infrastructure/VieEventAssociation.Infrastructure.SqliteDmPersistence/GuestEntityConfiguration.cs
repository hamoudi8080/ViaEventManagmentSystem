using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace VieEventAssociation.Infrastructure.SqliteDmPersistence;

public class GuestEntityConfiguration : IEntityTypeConfiguration<ViaGuest>
{
    
    public void Configure(EntityTypeBuilder<ViaGuest> builder)
    {
        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Id)
            .HasConversion(
                id => id.Value,
                value => GuestId.Create(value.ToString()).Payload);
        
        builder.Property<FirstName>("_FirstName")
            .HasColumnName("FirstName")
            .HasConversion(
                firstName => firstName.Value,
                value => FirstName.Create(value).Payload);  
        
        builder.Property<LastName>("_LastName")
            .HasColumnName("LastName")
            .HasConversion(
                lastName => lastName.Value,
                value => LastName.Create(value).Payload);
        
        builder.Property<Email>("_Email")
            .HasColumnName("Email")
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Payload);
        
    }
}