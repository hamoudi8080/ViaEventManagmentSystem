using Microsoft.EntityFrameworkCore;
using ViaEventManagmentSytem.Core.QueryContracts.Contracts;
using ViaEventManagmentSytem.Core.QueryContracts.Queries;

namespace ViaEventManagmentSystem.Infrastructure.EfcQueries.Queries;

public class EventHasStatusDraft : IQueryHandler<EventHasStatusDraftPage.Query, EventHasStatusDraftPage.Answer>
{
    private readonly VeadatabaseProductionContext _context;

    public EventHasStatusDraft(VeadatabaseProductionContext context)
    {
        _context = context;
    }

    public async Task<EventHasStatusDraftPage.Answer> HandleAsync(EventHasStatusDraftPage.Query query)
    {
        // Fetch the events that have a status of "Draft"
        var draftEvents = await _context.ViaEvents
            .Where(e => e.EventStatus == "draft")
            .ToListAsync();

        // Filter the draftEvents based on the SearchedText in the query
        var filteredEvents = draftEvents
            .Where(e => e.EventTitle.Contains(query.SearchedText))
            .Select(e => new EventHasStatusDraftPage.Event(
                e.Id,
                e.EventTitle ?? "",
                e.Description ?? "",
                DateTime.TryParse(e.StartDateTime, out var startDate) ? startDate : DateTime.MinValue,
                DateTime.TryParse(e.EndDateTime, out var endDate) ? endDate : DateTime.MinValue,
                e.MaxNumberOfGuests?.ToString() ?? "",
                e.EventVisibility ?? "",
                e.EventStatus ?? "",
                e.Guests.Count.ToString()
            )).ToList();

        // Calculate the maximum page number
        var maxPageNum = filteredEvents.Count / query.PageSize;
        if (filteredEvents.Count % query.PageSize > 0)
        {
            maxPageNum++;
        }

        // Create an instance of UpcomingEventsPage.Answer with the filtered events
        var answer = new EventHasStatusDraftPage.Answer(filteredEvents, maxPageNum);

        return answer;
        
    }
}
 