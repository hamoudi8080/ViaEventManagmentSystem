using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Repository;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public interface IViaEventRepository : IRepository<ViaEvent, EventId>
{
    
}