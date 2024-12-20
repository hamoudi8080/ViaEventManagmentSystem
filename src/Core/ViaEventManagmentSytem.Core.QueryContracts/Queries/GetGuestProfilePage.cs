using ViaEventManagmentSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagmentSytem.Core.QueryContracts.Queries;

public abstract class GetGuestProfilePage
{
    public record Query(string GuestId) : IQuery<Answer>;
    public record Answer(Guest Guest);
    
    public record Guest(
        string Id,
        string FirstName,
        string LastName,
        string Email
    );
}