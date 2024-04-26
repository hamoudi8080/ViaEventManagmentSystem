using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.Entities.ValueObjects;

public class InvitationStatus : Enumeration
{
    public static readonly InvitationStatus Accepted = new InvitationStatus(1, "Accepted");
    public static readonly InvitationStatus Pending = new InvitationStatus(0, "Pending");
    public static readonly InvitationStatus Declined = new InvitationStatus(2, "Declined");

    private InvitationStatus()
    {
    }

    private InvitationStatus(int value, string displayName) : base(value, displayName)
    {
    }

    /*

    private readonly string backingValue;

    private InvitationStatus(string value)
        => backingValue = value;


    private bool Equals(InvitationStatus other)
        => backingValue == other.backingValue;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((InvitationStatus)obj);
    }

    public override int GetHashCode()
        => backingValue.GetHashCode();
        */
}