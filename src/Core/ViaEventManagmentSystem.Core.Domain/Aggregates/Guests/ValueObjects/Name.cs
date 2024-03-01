using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    // ask teach about this. value-object must have only one property what if they have two?
    public Name(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.");

        FirstName = firstName;
        LastName = lastName;
    }

    //The GetEqualityComponents() method is an overridden method from the ValueObject base class.
    //This method is crucial for defining how equality is determined for instances of the value object.
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}