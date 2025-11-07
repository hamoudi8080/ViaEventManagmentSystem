namespace ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

/// <summary>
/// Default no-op implementation of IUnitOfWork for domain layer (testing purposes)
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}