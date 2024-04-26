using ViaEventManagmentSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagmentSytem.Core.QueryContracts.QueryDispatching;


public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TAnswer> DispatchAsync<TAnswer>(IQuery<TAnswer> query)
    {
        Type queryInterfaceWithType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TAnswer));
        dynamic handler = _serviceProvider.GetService(queryInterfaceWithType);

        if (handler == null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        return handler.HandleAsync((dynamic)query);
    }
}