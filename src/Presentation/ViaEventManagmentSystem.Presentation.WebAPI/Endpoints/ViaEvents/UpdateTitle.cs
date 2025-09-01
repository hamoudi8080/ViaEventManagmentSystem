using Microsoft.AspNetCore.Mvc;
using ViaEventManagementSystem.Core.AppEntry.Commands.Event;
using ViaEventManagementSystem.Core.AppEntry.Dispatcher;
using ViaEventManagmentSystem.Core.Tools.OperationResult;
using ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.Common;

namespace ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.ViaEvents;

public class UpdateTitle(ICommandDispatcher dispatcher) : ApiEndpoint.WithRequest<UpdateTitleRequest>.WithoutResponse
{
    [HttpPost("events/{Id}/update-title")]
    public override async Task<ActionResult> HandleAsync([FromRoute] UpdateTitleRequest request)
    {
        Result<UpdateEventTitleCommand> cmdResult =
            UpdateEventTitleCommand.Create(request.Id, request.RequestBody.Title);
        if (cmdResult.IsSuccess)
        {
            return BadRequest(cmdResult.ErrorMessage);
        }

        Result  result = await dispatcher.DispatchAsync(cmdResult.Payload);
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }
}

public class UpdateTitleRequest
{
    [FromRoute] public string Id { get; set; }
    [FromBody] public Body RequestBody { get; set; }

    public record Body(string Title);
}