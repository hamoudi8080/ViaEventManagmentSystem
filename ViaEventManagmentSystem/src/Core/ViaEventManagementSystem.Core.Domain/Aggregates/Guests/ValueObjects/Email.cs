using System.Text.RegularExpressions;
using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Value = address;
    }

    public string Value { get; }

    public static Result<Email> Create(string input)
    {
        // Validate email format
        if (!IsValidEmailAddress(input))
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.Email.TextFormatInvalid));

        // Split the email address into parts
        var parts = input.Split('@');

        // Check if the email address ends with "@via.dk"
        if (parts.Length != 2 || !parts[1].Equals("via.dk", StringComparison.OrdinalIgnoreCase))
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.Email.EmailMustEndWithViaDK));

        // Check the length and format of text1
        var text1 = parts[0];
        if (text1.Length < 3 || text1.Length > 6)
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.Email.TextLengthOutOfRange));

        var isValidEmail = ((text1.Length == 3 || text1.Length == 4) && text1.All(char.IsLetter))
                           || (text1.Length == 6 && text1.All(char.IsDigit));

        if (!isValidEmail)
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.Email.TextFormatInvalid));

        // Check if text1 consists of either 3 or 4 uppercase/lowercase English letters, or 6 digits from 0 to 9
        if (!(Regex.IsMatch(text1, "^[a-zA-Z]{3,4}$") || Regex.IsMatch(text1, "^[0-9]{6}$")))
            return Result<Email>.Failure(Error.BadRequest(ErrorMessage.Email.TextFormatInvalid));

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
        return Regex.IsMatch(email,
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}