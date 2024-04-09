namespace ViaEventManagmentSystem.Core.Domain.Common.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}