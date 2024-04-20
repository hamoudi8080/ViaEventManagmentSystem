using ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Infrastracure.SqliteDataWrite.UnitOfWork;

public class SqliteUnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}