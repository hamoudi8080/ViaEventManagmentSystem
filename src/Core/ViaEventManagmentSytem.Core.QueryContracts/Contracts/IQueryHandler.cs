namespace ViaEventManagmentSytem.Core.QueryContracts.Contracts;

public interface IQueryHandler<TQuery, TAnswer>
{
    public Task<TAnswer> HandleAsync(TQuery query);
}