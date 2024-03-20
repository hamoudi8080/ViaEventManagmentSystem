namespace ViaEventManagmentSystem.Core.Domain.Contracts;

public interface IViaRepository<T, ViaId>
{
    public Result<T> GetById(ViaId id);
    public Result<T> Add(T entity);
    public Result<T> Find(ViaId id);
    public Result Remove(ViaId id);
    public Result<T> Update(T entity);
   
}