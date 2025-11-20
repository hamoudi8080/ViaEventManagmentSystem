using ViaEventManagementSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagementSytem.Core.QueryContracts.Queries;

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