using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Domain.Common.Values;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }

    // ask teach about this. value-object must have only one property what if they have two?
    public LastName(string firstName)
    {
        ValidateLastName(firstName);
        Value = firstName;
    }

    public static Result<LastName> Create(string lastName)
    {
        if (!ValidateLastName(lastName))
            return Result<LastName>.Failure(
                Error.BadRequest(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));
        string formattedFirstName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();
        
        if (ContainsNumbers(lastName))
        {
            return Result<LastName>.Failure(Error.BadRequest(ErrorMessage.LastNameCannotContainNumbers));
        }

        if (ContainsSymbols(lastName))
        {
            return Result<LastName>.Failure(Error.BadRequest(ErrorMessage.LastNameCannotContainSymbols));
        }
        return Result<LastName>.Success(new LastName(formattedFirstName));
    }
    //The GetEqualityComponents() method is an overridden method from the ValueObject base class.
    //This method is crucial for defining how equality is determined for instances of the value object.

    private static bool ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return false;

        if (lastName.Length < 2 || lastName.Length > 25)
            return false;

        return true;
    }
    private static bool ContainsNumbers(string lastname)
    {
        return lastname.Any(char.IsDigit);
    }
    private static bool ContainsSymbols(string lastname)
    {
        return lastname.Any(c => !char.IsLetter(c));
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}