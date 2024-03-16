﻿using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.GuestTests.GuestFactory;

public abstract class GuestFactory
{
    public static Guest CreateEmptyGuest()
    {
        var id = GuestId.Create();
        var createGuest = Guest.Create(id);
        return createGuest.Payload;
    }
    
    /*
    public static Guest CreateGuest()
    {
        var id = GuestId.Create();
        var firstName = FirstName.Create("John").Payload;
        var lastname = LastName.Create("Resho").Payload;
        var email = Email.Create("John@via.dk").Payload;
        
        var createGuest = Guest.Create(id,firstName,lastname,email);
        return createGuest.Payload;
    }
    */
}