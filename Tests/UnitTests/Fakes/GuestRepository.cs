using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;
using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using IGuestRepository = ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.IGuestRepository;

namespace UnitTests.Fakes;

public class GuestRepository : IGuestRepository
{
    public List<Guest> _Guests { get; set; } = new();

    //Todo: ask, how how to get _Events.FirstOrDefault(x => x.Id == id) instead what is below
    public Task<Guest?> GetById(GuestId id)
    {
        return Task.FromResult(_Guests.FirstOrDefault(x => x._Id.Value == id.Value));
    }

    public Task<Guest> Add(Guest entity)
    {
        _Guests.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Guest> GetAll()
    {
        return Task.FromResult(_Guests.FirstOrDefault());
    }

    public Task<Guest> Find(GuestId id)
    {
        return null;
    }

    public Task Remove(GuestId id)
    {
        return Task.CompletedTask;
    }

    public Task<Guest> Update(Guest entity)
    {
        return Task.FromResult(entity);
    }
}