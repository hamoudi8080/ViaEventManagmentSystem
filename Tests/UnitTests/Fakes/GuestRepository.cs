using ViaEventManagementSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using IGuestRepository = ViaEventManagementSystem.Core.Domain.Aggregates.Guests.IGuestRepository;

namespace UnitTests.Fakes;

public class GuestRepository : IGuestRepository
{
    public List<ViaGuest> _Guests { get; set; } = new();

    //Todo: ask, how how to get _Events.FirstOrDefault(x => x.Id == id) instead what is below
    public Task<ViaGuest?> GetById(GuestId id)
    {
        return Task.FromResult(_Guests.FirstOrDefault(x => x.Id.Value == id.Value));
    }

    public Task Add(ViaGuest entity)
    {
        _Guests.Add(entity);
        return Task.FromResult(entity);
    }

    public async Task<IEnumerable<ViaGuest>> GetAll()
    {
        return null;
    }

    public Task<ViaGuest> Find(GuestId id)
    {
        return null;
    }

    public Task Remove(GuestId id)
    {
        return Task.CompletedTask;
    }

    public Task<ViaGuest> Update(ViaGuest entity)
    {
        return Task.FromResult(entity);
    }
}