using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Repository;
using ViaEventManagementSystem.Core.Domain.Common.Values;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events;

public interface IViaEventRepository : IRepository<ViaEvent, EventId>
{
   
    public Task Remove(EventId id);
}
