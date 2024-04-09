using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Repository;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Events;

public interface IViaEventRepository : IRepository<ViaEvent, EventId>
{
    public Task<ViaEvent> Find(ViaId id);
    public Task Remove(ViaId id);
}