using AIWorkHub.Application.Features.Notifications.Delete;
using AIWorkHub.Application.Features.Notifications.GetAll;
using AIWorkHub.Application.Features.Notifications.GetUnreadCount;
using AIWorkHub.Application.Features.Notifications.MarkAllAsRead;
using AIWorkHub.Application.Features.Notifications.MarkAsRead;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class NotificationsController(ISender sender)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result =
            await sender.Send(
                new GetNotificationsQuery(),
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount(
        CancellationToken cancellationToken)
    {
        var result =
            await sender.Send(
                new GetUnreadNotificationCountQuery(),
                cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new MarkNotificationAsReadCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new MarkAllNotificationsAsReadCommand(),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteNotificationCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return NoContent();
    }
}
