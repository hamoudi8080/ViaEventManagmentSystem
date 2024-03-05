using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventId : ValueObject
{
    
    public Guid Value { get; }

    public EventId(Guid id)
    {
        Value = id;
    }

    public static EventId Create()
    {
        return new EventId(Guid.NewGuid());
    }

    // Factory method to create a GuestId from string
    public static Result<EventId> Create(string id)
    {
        bool canBeParsed = Guid.TryParse(id, out Guid guid);
        return canBeParsed ? Result<EventId>.Success(new EventId(guid)) : Result<EventId>.Failure(Error.AddCustomError("Problems occured during create a guid id for gueest"));
    }
    // Equality comparison logic
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

     
}