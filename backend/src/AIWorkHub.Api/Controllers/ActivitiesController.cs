using AIWorkHub.Application.Features.Activities.GetByEntity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/activities")]
public sealed class ActivitiesController(ISender sender)
    : ControllerBase
{
    [HttpGet("{entityId:guid}")]
    public async Task<IActionResult> Get(
        Guid entityId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetActivitiesQuery(entityId),
            cancellationToken);

        return Ok(result);
    }
}
