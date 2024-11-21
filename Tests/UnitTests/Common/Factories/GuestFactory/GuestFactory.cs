using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Common.Factories.GuestFactory;

public abstract class GuestFactory
{
    
    public static GuestId ValidGuestId()
    {
        var id = GuestId.Create();
        return id.Payload;
    }
    public static Guest CreateEmptyGuest()
    {
        var id = GuestId.Create();
        var createGuest = Guest.Create(id.Payload);
        return createGuest.Payload;
    }
    
    
    public static Guest CreateGuest()
    {
        var id = GuestId.Create();
        var firstName = FirstName.Create("John").Payload;
        var lastname = LastName.Create("Resho").Payload;
        var email = Email.Create("John@via.dk").Payload;
        
        var createGuest = Guest.Create(id.Payload,firstName,lastname,email);
       
        return createGuest.Payload;
    }
    
}