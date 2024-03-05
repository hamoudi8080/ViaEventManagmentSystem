 
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;



public class FirstName : ValueObject
{
    
    public string Value { get; }
   
    // ask teach about this. value-object must have only one property what if they have two?
    public FirstName(string firstName)
    {
       
        Value = firstName;
       
    }

    public static Result<FirstName> Create(string firstName)
    {
        if (!ValidateFirstName(firstName))
        {
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.FirstNameMustBeBetween2And30CharsOrIsNullOrWhiteSpace));
        }
    
        return Result<FirstName>.Success(new FirstName(firstName));
    }
    
    private static bool ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return false;
        
        if (firstName.Length < 3 || firstName.Length > 30)
            return false;

        return true;
    }
    
    //The GetEqualityComponents() method is an overridden method from the ValueObject base class.
    //This method is crucial for defining how equality is determined for instances of the value object.
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}