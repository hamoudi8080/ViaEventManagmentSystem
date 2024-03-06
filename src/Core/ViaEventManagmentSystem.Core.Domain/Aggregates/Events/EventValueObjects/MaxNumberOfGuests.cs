using System.ComponentModel.DataAnnotations;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class MaxNumberOfGuests : ValueObject
{
    public int Value { get; private init; }


    public MaxNumberOfGuests(int maxNumber)
    {
        Value = maxNumber;
    }

    public static Result<MaxNumberOfGuests> Create(int maxNumber)
    {
        if (maxNumber <=4 || maxNumber >= 50)
        {
            return Result<MaxNumberOfGuests>.Failure(Error.BadRequest(ErrorMessage.MaxGuestsNoMustBeWithin4and50));
        }

        return Result<MaxNumberOfGuests>.Success(new MaxNumberOfGuests(maxNumber));
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}