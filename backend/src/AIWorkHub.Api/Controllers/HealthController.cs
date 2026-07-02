using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

/// <summary>
/// Provides API health status.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    /// <summary>
    /// Gets a lightweight health response for API consumers.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<HealthResponse> Get()
    {
        return Ok(new HealthResponse("Healthy", "AIWorkHub.Api", DateTimeOffset.UtcNow));
    }
}

/// <summary>
/// Represents the API health response.
/// </summary>
/// <param name="Status">The current health status.</param>
/// <param name="Service">The service name.</param>
/// <param name="Timestamp">The response timestamp.</param>
public sealed record HealthResponse(string Status, string Service, DateTimeOffset Timestamp);
