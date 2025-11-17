using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventId : ValueObject
{
    private EventId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    public static Result<EventId> Create()
    {
        return new EventId(Guid.NewGuid());
    }

    public static Result<EventId> Create(string id)
    {
        var canBeParsed = Guid.TryParse(id, out var guid);
        return canBeParsed
            ? Result<EventId>.Success(new EventId(guid))
            : Result<EventId>.Failure(Error.AddCustomError("Problems occured during create an event guid id "));
    }

    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}