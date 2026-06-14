using System.Security.Claims;
using Application.Features.Identity.Commands.RegisterUser;
using Application.Features.Identity.Commands.RevokeToken;
using Application.Features.Identity.Queries.GenerateToken;
using Application.Features.Identity.Queries.GetUserInfo;
using Application.Features.Identity.Queries.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("identity")]

public class IdentityController(ISender sender) : ControllerBase
{
    [HttpPost("token/generate")]
    public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("current-user/claims")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var result = await sender.Send(new GetUserByIdQuery(userId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("user/register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request, cancellationToken);

        if (result.IsSuccess) return Ok();

        return Conflict();
    }

    [HttpPost("token/revoke")]
    [Authorize]
    public async Task<IActionResult> RevokeToken(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Unauthorized("User Not Found");

        await sender.Send(new RevokeTokenCommand(userId), cancellationToken);

        return NoContent();
    }
}