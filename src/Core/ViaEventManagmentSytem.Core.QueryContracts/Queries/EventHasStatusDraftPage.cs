using ViaEventManagmentSytem.Core.QueryContracts.Contracts;

namespace ViaEventManagmentSytem.Core.QueryContracts.Queries;

public class EventHasStatusDraftPage
{

    public record Query(int PageNumber, int PageSize, string SearchedText) : IQuery<Answer>;
    public record Answer(List<Event> Events, int MaxPageNum);
    
    public record Event(
        string Id, 
        string EventTitle,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        string MaxNumberOfGuests,
        string EventVisibility,
        string Status,
        string Guests
    );
}