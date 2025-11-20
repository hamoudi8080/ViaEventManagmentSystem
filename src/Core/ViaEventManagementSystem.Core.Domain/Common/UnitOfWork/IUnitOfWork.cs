namespace ViaEventManagementSystem.Core.Domain.Common.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}