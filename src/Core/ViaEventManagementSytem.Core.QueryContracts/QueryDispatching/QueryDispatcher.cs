using ViaEventManagementSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagementSytem.Core.QueryContracts.QueryDispatching;


public class QueryDispatcher (IServiceProvider serviceProvider) : IQueryDispatcher

{
    public Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query)
    {
        Type queryInterfaceWithType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TAnswer));
        dynamic handler = serviceProvider.GetService(queryInterfaceWithType)!;

        if (handler == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        return handler.HandleAsync((dynamic)query);
    }
}