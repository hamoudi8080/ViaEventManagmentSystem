using ViaEventManagmentSytem.Core.QueryContracts.Contracts;
using ViaEventManagmentSytem.Core.QueryContracts.Queries;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries.Queries;

public class GetGuestProfile : IQueryHandler<GetGuestProfilePage.Query, GetGuestProfilePage.Answer >
{
 
    private readonly VeadatabaseProductionContext _context;

    public GetGuestProfile(VeadatabaseProductionContext context)
    {
        _context = context;
    }

    
    public Task<GetGuestProfilePage.Answer> HandleAsync(GetGuestProfilePage.Query query)
    {

        var guest = _context.Guests
            .Where(g => g.Id == query.GuestId)  
            .Select(g => new GetGuestProfilePage.Guest(
                g.Id,
                g.FirstName,
                g.LastName,
                g.Email
            )).FirstOrDefault();

        if (guest != null)
        {
            var answer = new GetGuestProfilePage.Answer(guest);
            return Task.FromResult(answer);
        }
        else
        {
            return Task.FromResult(new GetGuestProfilePage.Answer(null!));
        }
    }
}
 