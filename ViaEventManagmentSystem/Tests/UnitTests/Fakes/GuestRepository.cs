using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using IGuestRepository = ViaEventManagementSystem.Core.Domain.Aggregates.Guests.IGuestRepository;

namespace UnitTests.Fakes;

public class GuestRepository : IGuestRepository
{
    public List<ViaGuest> Guests { get; set; } = new();

    //Todo: ask, how how to get _Events.FirstOrDefault(x => x.Id == id) instead what is below

    public Task<ViaGuest?> GetAsync(Guid id)
    {
        return Task.FromResult(Guests.FirstOrDefault(x => x.Id.Value == id));
    }

    public Task RemoveAsync(Guid id)
    {
        var toRemove = Guests.FirstOrDefault(x => x.Id.Value == id);
        if (toRemove != null) Guests.Remove(toRemove);
        return Task.CompletedTask;
    }

    public Task AddAsync(ViaGuest aggregate)
    {
        Guests.Add(aggregate);
        return Task.FromResult(aggregate);
    }
}