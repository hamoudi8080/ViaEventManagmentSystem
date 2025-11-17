using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventTitle : ValueObject
{
    private EventTitle(string title)
    {
        Value = title;
    }

    public string Value { get; }

    public static Result<EventTitle> Create(string? title)
    {
        if (!ValidateEventTitle(title))
            return Result<EventTitle>.Failure(Error.BadRequest(ErrorMessage.EventFields.TitleLengthOutOfRange));

        return Result<EventTitle>.Success(new EventTitle(title));
    }


    private static bool ValidateEventTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (title.Length < 3 || title.Length > 75)
            return false;

        return true;
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}