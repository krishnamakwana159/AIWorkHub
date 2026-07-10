using AIWorkHub.Application.Features.Search.GlobalSearch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class SearchController(ISender sender)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] string q,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new SearchQuery(q),
            cancellationToken);

        return Ok(result);
    }
}
