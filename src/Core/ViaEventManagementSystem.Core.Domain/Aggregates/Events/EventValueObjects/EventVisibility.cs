using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects.Util;
using ViaEventManagmentSystem.Core.Tools.OperationResult;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;

/*
public class EventVisibility : Enumeration {


    public static readonly EventVisibility Private = new EventVisibility(0, "Private");
    public static readonly EventVisibility Public = new EventVisibility(1, "Public");


    private EventVisibility(){}

    private EventVisibility(int value, string displayName): base(value, displayName){}
    
    
    public static EventVisibility From(EventVisibilityEnum visibilityEnum)
    {
        switch (visibilityEnum)
        {
            case EventVisibilityEnum.Private:
                return Private;
            case EventVisibilityEnum.Public:
                return Public;
            default:
                throw new ArgumentException("Invalid visibility enum value", nameof(visibilityEnum));
        }
    }
}
*/

public enum EventVisibility
{
    Private,
    Public 
}