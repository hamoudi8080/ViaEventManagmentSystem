using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;

public class EventVisibility : Enumeration {


    internal static readonly EventVisibility Private = new EventVisibility(0, "Private");
    internal static readonly EventVisibility Public = new EventVisibility(1, "Public");


    private EventVisibility(){}

    private EventVisibility(int value, string displayName): base(value, displayName){}
    
}