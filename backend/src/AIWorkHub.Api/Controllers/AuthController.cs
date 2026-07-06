using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Application.Features.Authentication.Login;
using AIWorkHub.Application.Features.Authentication.RefreshToken;
using AIWorkHub.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IMediator mediator): ControllerBase
{

   [HttpPost("register")]
   public async Task <IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
   {
      var result = await mediator.Send(new RegisterCommand(request), cancellationToken);

      if (result.IsFailure) {
         return BadRequest(result.Errors);
      }

      return Ok(result.Value);
   }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand(request), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshTokenCommand(request), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }

}
