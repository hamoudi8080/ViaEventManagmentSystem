using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EndDateTime : ValueObject
{
    private EndDateTime(DateTime dateTime)
    {
        Value = dateTime;
    }

    public DateTime Value { get; }

    public static Result<EndDateTime> Create(DateTime dateTime)
    {
        var validationResult = ValidateEndDateTime(dateTime);
        if (!validationResult.IsSuccess)
            return Result<EndDateTime>.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));

        return Result<EndDateTime>.Success(new EndDateTime(dateTime));
    }

    private static Result ValidateEndDateTime(DateTime dateTime)
    {
        // Check if the hour is before 08:00 on the same day
        if (dateTime.Hour < 8 && dateTime.Date == dateTime.Date.AddDays(1))
            return Result.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));

        // Check if end time is before 23:59
        if (dateTime.TimeOfDay > TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59)))
            return Result.Failure(Error.BadRequest(ErrorMessage.General.InvalidInput));

        return Result.Success();
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}