using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;

public class Guest : Aggregate<GuestId>
{
    private GuestId Id { get;  }
    public Name Name { get; private set; }
    public Email Email { get; private set; }

   

    public Guest(Name name, GuestId id, Email email) 
    {
        if (name == null)
            throw new ArgumentNullException(nameof(name));
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        Name = name;
        Email = email;
    }

    public void UpdateEmail(Email newEmail)
    {
        if (newEmail == null)
            throw new ArgumentNullException(nameof(newEmail));

        Email = newEmail;
    }

    public void UpdateFirstName(string newFirstName)
    {
        if (string.IsNullOrWhiteSpace(newFirstName))
            throw new ArgumentException("First name cannot be null or empty.");

        Name = new Name(newFirstName, Name.LastName);
    }

    public void UpdateLastName(string newLastName)
    {
        if (string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentException("Last name cannot be null or empty.");

        Name = new Name(Name.FirstName, newLastName);
    }
}