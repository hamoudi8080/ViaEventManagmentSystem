using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Domain.Common.Values;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string address)
    {
        Value = address;
    }

    public static Result<Email> Create(string input)
    {
        
        if (!IsValidEmailAddress(input))
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.InvalidEmailAddress));

        return Result<Email>.Success(new Email(input));
    }


    // Define equality components for the email address
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    // Validate email address format using a simple regex
    private static bool IsValidEmailAddress(string email)
    {
        // Simple email validation regex for demonstration purposes
        return System.Text.RegularExpressions.Regex.IsMatch(email,
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}