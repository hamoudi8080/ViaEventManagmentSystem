using Microsoft.AspNetCore.Mvc;

namespace ViaEventManagmentSystem.Presentation.WebAPI.Endpoints.ViaEvents;

[ApiController]
[Route("api")]
public class HelloController
{
    [HttpGet("hello")]
    public ActionResult<string> Hello()
    {
        return "Hello from Create Event Endpoint!";
    }
}