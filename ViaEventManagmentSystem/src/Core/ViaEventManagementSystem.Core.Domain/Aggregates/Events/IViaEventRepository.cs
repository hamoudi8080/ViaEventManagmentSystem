using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Repository;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Events;

public interface IViaEventRepository : IRepository<ViaEvent, EventId>
{
}