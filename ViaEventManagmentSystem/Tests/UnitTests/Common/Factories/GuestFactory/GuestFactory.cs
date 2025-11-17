using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Common.Factories.GuestFactory;

public abstract class GuestFactory
{
    public static GuestId ValidGuestId()
    {
        var id = GuestId.Create();
        return id.Payload;
    }

    public static ViaGuest CreateEmptyGuest()
    {
        var id = GuestId.Create();
        var createGuest = ViaGuest.Create(id.Payload);
        return createGuest.Payload;
    }


    public static ViaGuest CreateGuest()
    {
        var id = GuestId.Create();
        var firstName = FirstName.Create("John").Payload;
        var lastname = LastName.Create("Resho").Payload;
        var email = Email.Create("John@via.dk").Payload;

        var createGuest = ViaGuest.Create(id.Payload, firstName, lastname, email);

        return createGuest.Payload;
    }
}