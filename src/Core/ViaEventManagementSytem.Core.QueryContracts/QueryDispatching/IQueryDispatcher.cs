using ViaEventManagementSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagementSytem.Core.QueryContracts.QueryDispatching;

public interface IQueryDispatcher
{
    Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query);
}