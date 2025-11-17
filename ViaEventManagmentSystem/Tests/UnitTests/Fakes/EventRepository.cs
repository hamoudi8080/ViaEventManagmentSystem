using ViaEventManagementSystem.Core.Domain.Aggregates.Events;

namespace UnitTests.Fakes;

public class EventRepository : IViaEventRepository
{
    public List<ViaEvent> _Events { get; set; } = new();


    public Task<ViaEvent?> GetAsync(Guid id)
    {
        var found = _Events.FirstOrDefault(x => x._eventId.Value == id);
        return Task.FromResult(found);
    }

    public Task RemoveAsync(Guid id)
    {
        var toRemove = _Events.FirstOrDefault(x => x._eventId.Value == id);
        if (toRemove != null) _Events.Remove(toRemove);
        return Task.CompletedTask;
    }

    public Task AddAsync(ViaEvent aggregate)
    {
        _Events.Add(aggregate);
        return Task.FromResult(aggregate);
    }
}