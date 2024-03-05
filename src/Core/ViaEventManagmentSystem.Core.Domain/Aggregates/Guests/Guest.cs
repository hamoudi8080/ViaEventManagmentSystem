using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;

public class Guest : Aggregate<GuestId>
{
    
    internal  GuestId Id { get;  }
    internal  FirstName FirstName { get; set; }
    internal  LastName LastName { get; set; }
    internal  Email Email { get; set; }

   

    public Guest(FirstName fname,LastName lName, Email email) 
    {
       
        Id = GuestId.Create();
        FirstName = fname;
        LastName = lName;
        Email = email;
    }

    public Result<Guest> UpdateEmail(Email email)
    {
        if (email == null)
            return Result<Guest>.Failure(Error.AddCustomError("Email is null"));

        return Result<Guest>.Success(this);
    }

    /*
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
    */


    public static Guest CreateGuest(FirstName firstName, LastName lastName, Email email)
    {
        return new Guest(firstName, lastName, email);
    }
}