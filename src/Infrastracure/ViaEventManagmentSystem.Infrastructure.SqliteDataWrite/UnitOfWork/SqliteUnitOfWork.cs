using ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

namespace ViaEventManagmentSystem.Infrastructure.SqliteDataWrite.UnitOfWork;

public class SqliteUnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync() => context.SaveChangesAsync();
}