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
        ValidateFirstName(firstName);
        Value = firstName;
       
    }

    public Result<FirstName> Create(string firstName, string lastName)
    {
        if (!ValidateFirstName(firstName))
            return Result<FirstName>.Failure(Error.BadRequest(ErrorMessage.LastNameMustBeBetween2And30CharsOrIsNullOrWhiteSpace));
        
        return Result<FirstName>.Success(new FirstName(firstName ));
    }
    //The GetEqualityComponents() method is an overridden method from the ValueObject base class.
    //This method is crucial for defining how equality is determined for instances of the value object.
    
    private bool ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return false;
        
        if (firstName.Length < 3 || firstName.Length > 30)
            return false;

        return true;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}