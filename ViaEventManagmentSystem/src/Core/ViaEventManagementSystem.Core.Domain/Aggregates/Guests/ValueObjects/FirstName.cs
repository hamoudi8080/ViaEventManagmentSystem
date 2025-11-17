using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class FirstName : ValueObject
{
    private FirstName(string firstName)
    {
        Value = firstName;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<FirstName>.Failure(
                Error.BadRequest(ErrorMessage.PersonName.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));

        var trimmed = firstName.Trim();

        // Numbers not allowed
        if (ContainsNumbers(trimmed))
            return Result<FirstName>.Failure(
                Error.BadRequest(ErrorMessage.PersonName.FirstNameCannotContainNumbers));

        // Symbols not allowed (only letters allowed)
        if (ContainsSymbols(trimmed))
            return Result<FirstName>.Failure(
                Error.BadRequest(ErrorMessage.PersonName.FirstNameCannotContainSymbols));

        // Length rule after trimming
        if (!ValidateFirstName(trimmed))
            return Result<FirstName>.Failure(
                Error.BadRequest(ErrorMessage.PersonName.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));

        // Capitalize: first letter upper, rest lower
        var formatted = char.ToUpper(trimmed[0]) + trimmed.Substring(1).ToLower();

        return Result<FirstName>.Success(new FirstName(formatted));
    }


    private static bool ValidateFirstName(string firstName)
    {
        if (firstName.Length < 2 || firstName.Length > 25)
            return false;
        return true;
    }

    private static bool ContainsNumbers(string firstName)
    {
        return firstName.Any(char.IsDigit);
    }

    private static bool ContainsSymbols(string firstName)
    {
        return firstName.Any(c => !char.IsLetter(c));
    }
    //The GetEqualityComponents() method is an overridden method from the ValueObject base class.
    //This method is crucial for defining how equality is determined for instances of the value object.

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}