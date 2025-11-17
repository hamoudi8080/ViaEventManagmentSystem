using Microsoft.AspNetCore.Mvc;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher;
using ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.Common;
using EventId = ViaEventManagementSystem.Core.Domain.Aggregates.Events.EventValueObjects.EventId;

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
        var eventTitle = "Test Event";
        var startDateTime = DateTime.Now.AddDays(1);
        var endDateTime = startDateTime.AddHours(2);
        var maxNumberOfGuests = 33;
        var eventDescription = "Test Event Description";


        var cmd = CreateEventCommand.Create(id.Payload.Value.ToString(), eventTitle, startDateTime, endDateTime,
            maxNumberOfGuests, eventDescription);

        var result = await _dispatcher.DispatchAsync(cmd.Payload);
        return result.IsSuccess
            ? Ok(new CreateEventResponse(cmd.Payload.ToString()))
            : BadRequest(result.ErrorMessage);
    }
}

public record CreateEventResponse(string? Id);