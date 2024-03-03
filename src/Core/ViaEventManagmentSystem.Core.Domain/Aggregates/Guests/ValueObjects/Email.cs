using ViaEventManagmentSystem.Core.Domain.Common.Values;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string address)
    {
        Value = address;
    }

    public static Result<Email> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result<Email>.Failure(new Error(0, "Invalid input provided."));

        if (!IsValidEmailAddress(input))
            return Result<Email>.Failure(new Error(0, "Invalid email format."));

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