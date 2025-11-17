using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventDescription : ValueObject
{
    private EventDescription(string eventDescription)
    {
        Value = eventDescription;
    }

    public string Value { get; }


    public static Result<EventDescription> Create(string eventDescription)
    {
        if (eventDescription.Length < 0 || eventDescription.Length > 250)
            return Result<EventDescription>.Failure(
                Error.BadRequest(ErrorMessage.EventFields.DescriptionLengthOutOfRange));
        return Result<EventDescription>.Success(new EventDescription(eventDescription));
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}