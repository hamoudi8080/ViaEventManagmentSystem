using ViaEventManagmentSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagmentSytem.Core.QueryContracts.QueryDispatching;

public interface IQueryDispatcher
{
    Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query);
}