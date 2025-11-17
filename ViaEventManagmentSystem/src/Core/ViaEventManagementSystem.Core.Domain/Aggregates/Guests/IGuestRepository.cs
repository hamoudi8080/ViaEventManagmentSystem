using ViaEventManagementSystem.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Repository;

namespace ViaEventManagementSystem.Core.Domain.Aggregates.Guests;

public interface IGuestRepository : IRepository<ViaGuest, GuestId>
{
    //Maybe relevant to find by username or email?
}