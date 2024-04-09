using ViaEventManagmentSystem.Core.Domain.Aggregates.Events;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace UnitTests.Fakes;

public class EventRepository : IViaEventRepository
{
    public List<ViaEvent> _Events { get; set; } = new();


    public Task<ViaEvent?> GetById(EventId id)
    {
        return Task.FromResult(_Events.FirstOrDefault(x => x._eventId.Value == id.Value));
    }

    public Task<ViaEvent> Add(ViaEvent entity)
    {
        _Events.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<ViaEvent> GetAll()
    {
        return Task.FromResult(_Events.FirstOrDefault());
    }

    public Task<ViaEvent> Find(ViaId id)
    {
        return null;
    }

    public Task Remove(ViaId id)
    {
        return Task.CompletedTask;
    }

    public Task<ViaEvent> Update(ViaEvent entity)
    {
        return Task.FromResult(entity);
    }
}