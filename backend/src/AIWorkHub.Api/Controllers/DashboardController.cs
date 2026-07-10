using AIWorkHub.Application.Features.Dashboard.GetDashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class DashboardController(ISender sender)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardQuery(),
            cancellationToken);

        return Ok(result);
    }
}
