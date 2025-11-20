using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects;
using ViaEventManagementSystem.Core.Domain.Common.Values;

namespace ViaEventManagementSystem.Core.Domain.Common.Repository;

public interface IRepository<T, in TId> where T : Aggregate<TId> {
    
    public Task<T> GetById(TId id);
    public Task  Add(T entity);
    
    
 
  
 

}