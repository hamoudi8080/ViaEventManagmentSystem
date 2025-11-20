using ViaEventManagementSystem.Core.Domain.Aggregates.Events;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Values;

namespace UnitTests.Fakes;

public class EventRepository : IViaEventRepository
{
    public List<ViaEvent> _Events { get; set; } = new();


    public Task<ViaEvent?> GetById(EventId id)
    {
        return Task.FromResult(_Events.FirstOrDefault(x => x._eventId.Value == id.Value));
    }

    public Task Add(ViaEvent entity)
    {
        _Events.Add(entity);
        return Task.FromResult(entity);
    }

    public async Task<IEnumerable<ViaEvent>> GetAll()
    {
        return null;
    }

    public Task Remove(EventId id)
    {
        return Task.CompletedTask;
    }

    public Task<ViaEvent> Find(ViaId id)
    {
        return null;
    }

    public Task<ViaEvent> Get(ViaId id)
    {
        throw new NotImplementedException();
    }

   
    public Task<ViaEvent> Update(ViaEvent entity)
    {
        return Task.FromResult(entity);
    }
}