using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.Entities.Invitation;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace VieEventAssociation.Infrastructure.SqliteDmPersistence;

public class EventEntityConfiguration : IEntityTypeConfiguration<ViaEvent>
{
    public void Configure(EntityTypeBuilder<ViaEvent> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => EventId.Create(value.ToString()).Payload);


        builder.Property<EventTitle>("_EventTitle")
            .HasColumnName("Title")
            .HasConversion(
                title => title.Value,
                value => EventTitle.Create(value).Payload);


        builder.Property<EventDescription>("_Description")
            .HasColumnName("Description")
            .HasConversion(
                //This is the "model-to-provider" conversion.
                //It tells EF Core that when it needs to save an EventDescription object to the database.
                description => description.Value,
                //This is the "provider-to-model" conversion.
                //It tells EF Core that when it reads a value from the database, it should create
                value => EventDescription.Create(value).Payload);

        builder.Property<StartDateTime>("_StartDateTime")
            .HasColumnName("StartDateTime")
            .HasConversion(
                startDateTime => startDateTime.Value,
                value => StartDateTime.Create(value).Payload);

        builder.Property<EndDateTime>("_EndDateTime")
            .HasColumnName("EndDateTime")
            .HasConversion(
                endDateTime => endDateTime.Value,
                value => EndDateTime.Create(value).Payload);

        builder.Property<MaxNumberOfGuests>("_MaxNumberOfGuests")
            .HasColumnName("MaxNumberOfGuests")
            .HasConversion(
                maxGuests => maxGuests.Value,
                value => MaxNumberOfGuests.Create(value).Payload);

        builder.Property<EventVisibility?>("_EventVisibility")
            .HasColumnName("Visibility")
            .HasConversion(
                v => v.HasValue ? v.Value.ToString() : null,
                v => v != null ? (EventVisibility)Enum.Parse(typeof(EventVisibility), v) : null);

        builder.Property<EventStatus>("_EventStatus")
            .HasColumnName("Status")
            .HasConversion(
                v => v.ToString(),
                v => (EventStatus)Enum.Parse(typeof(EventStatus), v));

        var invitationsNavigation = builder.Metadata.FindNavigation("_Invitations");
        invitationsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany<Invitation>("_Invitations")
            .WithOne()
            .HasForeignKey("_EventId")
            .IsRequired();

        var guestsNavigation = builder.Metadata.FindNavigation("_GuestsParticipants");
        guestsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany<GuestId>("_GuestsParticipants", pa =>
        {
            pa.ToTable("EventGuestParticipation");
            pa.Property(p => p.Value)
                .HasColumnName("GuestId")
                .HasConversion(
                    guestId => guestId,
                    value => value);
            pa.WithOwner().HasForeignKey("EventId");
            pa.HasKey("EventId", nameof(GuestId.Value));
        });
    }
}