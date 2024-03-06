using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventDescription : ValueObject
{
    internal string Value { get; private init; }

    public EventDescription(string eventDescription)
    {
        Value = eventDescription;
    }


    public static Result<EventDescription> Create(string eventDescription)
    {
        if (string.IsNullOrWhiteSpace(eventDescription))
            return Result<EventDescription>.Failure(Error.AddCustomError("The Description is null or white spaces"));

        if (eventDescription.Length < 1 || eventDescription.Length > 400)
        {
            return Result<EventDescription>.Failure(
                Error.AddCustomError("The Description Must be between 1 and 400 characters"));
        }

        return Result<EventDescription>.Success(new EventDescription(eventDescription));
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}