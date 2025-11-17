using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class StartDateTime : ValueObject
{
    private StartDateTime(DateTime dateTime)
    {
        Value = dateTime;
    }

    public DateTime Value { get; }

    public static Result<StartDateTime> Create(DateTime dateTime)
    {
        return Result<StartDateTime>.Success(new StartDateTime(dateTime));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}