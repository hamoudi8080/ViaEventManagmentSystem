using ViaEventManagmentSystem.Core.Domain.Common.Bases;
using ViaEventManagmentSystem.Core.Domain.Common.Repository;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite;

public class RepositoryEfcBase<TAgg,TId> (AppDbContext context) : IRepository<TAgg, TId> where TAgg : Aggregate<TId>
{
    public virtual async Task<TAgg> GetById(TId id)
    {
       TAgg? root = await context.Set<TAgg>().FindAsync();
       return root;
    }

    public virtual async Task Add(TAgg entity) => await context.Set<TAgg>().AddAsync(entity);
    
    public async Task<IEnumerable<TAgg>> GetAll()
    {
        throw new NotImplementedException();
    }

    public virtual async Task Remove(TId id)
    {
        TAgg? root = await context.Set<TAgg>().FindAsync(id);
        context.Set<TAgg>().Remove(root!);
    }
}
 