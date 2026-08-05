using AIWorkHub.Application.Features.Users.GetLookupUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class UsersController(
    ISender sender)
    : ControllerBase
{
    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup(
        CancellationToken cancellationToken)
    {
        var result =
            await sender.Send(
                new GetUserLookupQuery(),
                cancellationToken);

        return Ok(result);
    }
}
