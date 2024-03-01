using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be null or empty.");

        if (!IsValidEmailAddress(address))
            throw new ArgumentException("Invalid email address.");

        Value = address.ToLower(); // Normalize the email address to lowercase
    }

    // Define equality components for the email address
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    // Validate email address format using a simple regex
    private bool IsValidEmailAddress(string email)
    {
        // Simple email validation regex for demonstration purposes
        return System.Text.RegularExpressions.Regex.IsMatch(email,
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}