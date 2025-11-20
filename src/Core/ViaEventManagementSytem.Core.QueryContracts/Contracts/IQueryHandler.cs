namespace ViaEventManagementSytem.Core.QueryContracts.Contracts;

public interface IQueryHandler<TQuery, TAnswer> where TQuery : IQuery<TAnswer>
{
    public Task<TAnswer> HandleAsync(TQuery query);
}