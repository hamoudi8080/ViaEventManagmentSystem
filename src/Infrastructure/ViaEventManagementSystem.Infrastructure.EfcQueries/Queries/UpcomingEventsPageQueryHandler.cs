using ViaEventManagementSytem.Core.QueryContracts.Contracts;
using ViaEventManagementSytem.Core.QueryContracts.Queries;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ViaEventManagementSystem.Infrastructure.EfcQueries.Queries
{
    public class UpcomingEventsPageQueryHandler : IQueryHandler<UpcomingEventsPage.Query, UpcomingEventsPage.Answer>
    {
        
        private readonly VeadatabaseProductionContext _context;

        public UpcomingEventsPageQueryHandler(VeadatabaseProductionContext context)
        {
            _context = context;
        }

        
       
        public async Task<UpcomingEventsPage.Answer> HandleAsync(UpcomingEventsPage.Query query)
        {
            var upcomingEvents = await _context.ViaEvents
                .Where(e => e.EventTitle.Contains(query.SearchedText)).Include(viaEvent => viaEvent.GuestIds)
                .ToListAsync();

            var filteredEvents = upcomingEvents
                .Where(e => DateTime.Parse(e.StartDateTime) > DateTime.Now || DateTime.Parse(e.EndDateTime) > DateTime.Now);

            var upcomingEventsWith = filteredEvents.Select(e => new UpcomingEventsPage.Event(
                e.Id,
                e.EventTitle,
                e.Description,
                DateTime.Parse(e.StartDateTime),
                DateTime.Parse(e.EndDateTime),
                e.MaxNumberOfGuests.ToString(),
                e.EventVisibility,
                e.EventStatus,
                e.GuestIds.Count().ToString())).ToList();
            //calculate max page number
            var maxPageNum = upcomingEvents.Count() / query.PageSize;
            if (upcomingEvents.Count() % query.PageSize > 0)
            {
                maxPageNum++;
            }
            
            var upcomingEvent =  new UpcomingEventsPage.Answer(upcomingEventsWith, maxPageNum);
            return upcomingEvent;
        }
    }
}