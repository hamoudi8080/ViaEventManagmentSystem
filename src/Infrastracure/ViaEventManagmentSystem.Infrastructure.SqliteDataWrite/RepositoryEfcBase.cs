using ViaEventManagementSystem.Core.Domain.Common.Bases;
using ViaEventManagementSystem.Core.Domain.Common.Repository;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite;

public abstract class RepositoryEfcBase<TAgg,TId> (AppDbContext context) : IRepository<TAgg, TId> where TAgg : Aggregate<TId>
{
    public virtual async Task<TAgg> GetById(TId id)
    {
       TAgg? root = await context.Set<TAgg>().FindAsync(id);
       return root;
    }

    public virtual async Task Add(TAgg entity) => await context.Set<TAgg>().AddAsync(entity);
    
   

    public virtual async Task Remove(TId id)
    {
        TAgg? root = await context.Set<TAgg>().FindAsync(id);
        context.Set<TAgg>().Remove(root!);
    }
}
 