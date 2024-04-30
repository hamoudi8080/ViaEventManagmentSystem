using Microsoft.AspNetCore.Mvc;
using ViaEventManagmentSystem.Core.AppEntry.Commands.Event;
using ViaEventManagmentSystem.Core.AppEntry.Dispatcher;
using ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.Common;
using EventId = ViaEventManagmentSystem.Core.Domain.Aggregates.Events.EventValueObjects.EventId;

namespace ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.ViaEvents;

public class Create : ApiEndpoint.WithoutRequest.WithResponse<CreateEventResponse>
{
    private readonly ICommandDispatcher _dispatcher;

    public Create(ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("events/create")]
    public override async Task<ActionResult<CreateEventResponse>> HandleAsync()
    {
        
        var eid = Guid.NewGuid();
        var id = EventId.Create(eid.ToString());
        string eventTitle = "Test Event";
        DateTime startDateTime = DateTime.Now.AddDays(1);
        DateTime endDateTime = startDateTime.AddHours(2);
        int maxNumberOfGuests = 33;
        string eventDescription = "Test Event Description";

        
        var cmd = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime, maxNumberOfGuests, eventDescription);

        Result  result = await _dispatcher.DispatchAsync(cmd.Payload);
        return result.IsSuccess
            ? Ok(new CreateEventResponse(cmd.Payload.ViaEvent.Id.ToString()))
            : BadRequest(result.ErrorMessage);
    }
}

public record CreateEventResponse(string Id);