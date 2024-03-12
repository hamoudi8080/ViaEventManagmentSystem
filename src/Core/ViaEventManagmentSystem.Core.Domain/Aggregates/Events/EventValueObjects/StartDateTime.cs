using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class StartDateTime : ValueObject
{
    internal DateTime Value { get; private init; }

    private StartDateTime(DateTime dateTime) => Value = dateTime;

    public static Result<StartDateTime> Create(DateTime dateTime)
    {
        var validationResult = ValidateStartDateTime(dateTime);
        if (!validationResult.IsSuccess)
        {
            return Result<StartDateTime>.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));
        }

        return Result<StartDateTime>.Success(new StartDateTime(dateTime));
    }


    private static Result ValidateStartDateTime(DateTime dateTime)
    {
        // Check if the hour is before 08:00 or if it's after 24:00
        if (dateTime.Hour < 8 || dateTime.Hour >= 24)
            return Result.Failure(Error.BadRequest(ErrorMessage.InvalidInputError));

        // Check if the time is before 08:00
        if (dateTime.TimeOfDay < TimeSpan.FromHours(8))
            return Result.Failure(Error.BadRequest(ErrorMessage.EventCannotStartBefore8Am));

        return Result.Success();
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}