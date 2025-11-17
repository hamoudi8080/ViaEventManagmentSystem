using ViaEventManagementSystem.Core.Domain.Common.Bases;

namespace ViaEventManagementSystem.Core.Domain.Common.Repository;

public interface IRepository<T, in TId> where T : Aggregate<TId>
{
    Task<T?> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
    Task AddAsync(T aggregate);
}