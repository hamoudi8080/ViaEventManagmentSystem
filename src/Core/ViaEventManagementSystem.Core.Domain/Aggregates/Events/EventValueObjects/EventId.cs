using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagementSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventId : ValueObject
{
    public Guid Value { get; }

    private EventId(Guid id)
    {
        Value = id;
    }

    public static Result<EventId> Create()
    {
        return new EventId(Guid.NewGuid());
    }
    
    public static Result<EventId> Create(string id)
    {
        bool canBeParsed = Guid.TryParse(id, out Guid guid);
        return canBeParsed ? Result<EventId>.Success(new EventId(guid)) : Result<EventId>.Failure(Error.AddCustomError("Problems occured during create an event guid id "));
    }
    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

     
}