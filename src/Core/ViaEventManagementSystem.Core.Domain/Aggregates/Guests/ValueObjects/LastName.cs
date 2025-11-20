using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagementSystem.Core.Domain.Common.Values;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects; 
public sealed class LastName : ValueObject
{
    public string Value { get; }

    private LastName(string value) => Value = value;

    public static Result<LastName> Create(string? lastName)
    {
        // empty / null / whitespace → same consolidated error (what the test expects)
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result<LastName>.Failure(
                Error.BadRequest(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));
        }

        var trimmed = lastName.Trim();

        // numbers not allowed
        if (ContainsNumbers(trimmed))
        {
            return Result<LastName>.Failure(
                Error.BadRequest(ErrorMessage.PersonName.LastNameCannotContainNumbers));
        }

        // symbols not allowed (only letters). Relax if you want to allow '-' or '''
        if (ContainsSymbols(trimmed))
        {
            return Result<LastName>.Failure(
                Error.BadRequest(ErrorMessage.LastNameCannotContainSymbols));
        }

        // length rule after trimming → same consolidated error
        if (trimmed.Length < 2 || trimmed.Length > 25)
        {
            return Result<LastName>.Failure(
                Error.BadRequest(ErrorMessage.LastNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));
        }

        // Normalize casing
        string formatted = char.ToUpper(trimmed[0]) + trimmed.Substring(1).ToLower();

        return Result<LastName>.Success(new LastName(formatted));
    }

    private static bool ContainsNumbers(string s) => s.Any(char.IsDigit);
    private static bool ContainsSymbols(string s) => s.Any(c => !char.IsLetter(c));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
