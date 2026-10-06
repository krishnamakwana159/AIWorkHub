using AIWorkHub.Application.Features.Users.ChangeMyPassword;
using AIWorkHub.Application.Features.Users.GetLookupUsers;
using AIWorkHub.Application.Features.Users.GetMyProfile;
using AIWorkHub.Application.Features.Users.UpdateMyProfile;
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

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetMyProfileQuery(),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile(
        UpdateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateMyProfileCommand(request),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPut("me/password")]
    public async Task<IActionResult> ChangeMyPassword(
        ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new ChangeMyPasswordCommand(request),
            cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Errors);
    }
}
