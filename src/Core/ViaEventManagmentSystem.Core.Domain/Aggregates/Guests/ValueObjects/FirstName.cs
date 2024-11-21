 
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;



public class FirstName : ValueObject
{
    
    public string Value { get; }
    
    private FirstName(string firstName)
    {
       
        Value = firstName;
       
    }

    public static Result<FirstName> Create(string firstName)
    {
        if (firstName == "")
        {
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.FirstNameCannotBeNull));
        }
        if (!ValidateFirstName(firstName))
        {
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.FirstNameMustBeBetween2And25CharsOrIsNullOrWhiteSpace));
        }
        // Capitalize the first letter and lowercase the rest
        string formattedFirstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
        
        // Check if the first name contains numbers
        if (ContainsNumbers(firstName))
        {
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.FirstNameCannotContainNumbers));
        }
        if (ContainsSymbols(firstName))
        {
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.FirstNameCannotContainSymbols));
        }
        return Result<FirstName>.Success(new FirstName(formattedFirstName));
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