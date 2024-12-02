using ViaEventManagmentSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Repository;

namespace ViaEventManagmentSystem.Core.Domain.Aggregates.Guests;

public interface IGuestRepository : IRepository<ViaGuest, GuestId>
{
    public Task Remove(GuestId id);
}