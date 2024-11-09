using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Repository;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public interface IViaEventRepository : IRepository<ViaEvent, EventId>
{
   
    public Task Remove(EventId id);
}
