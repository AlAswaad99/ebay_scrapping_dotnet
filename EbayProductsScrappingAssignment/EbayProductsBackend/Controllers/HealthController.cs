using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HealthCheck()
    {
        return Ok("OK");
    }
}
