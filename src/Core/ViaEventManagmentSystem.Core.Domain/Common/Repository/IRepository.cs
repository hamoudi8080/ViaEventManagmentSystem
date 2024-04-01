using ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Domain.Common.Values;

namespace ViaEventManagmentSystem.Core.Domain.Common.Repository;

public interface IRepository<T, in TId> where TId : EventId
    where T : Aggregate<TId> {
    
    public Task<T> GetById(TId id);
    public Task<T> Add(T entity);
    public Task<T> Find(ViaId id);
    public Task Remove(ViaId id);
    public Task<T> Update(T entity);

}