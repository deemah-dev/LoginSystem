using System.Security.Claims;
using Application.Features.Errors;
using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Identity.Queries.RefreshToken;

public class RefreshTokenCommandHandler
(IAppDbContext context
, IIdentityService identityService
, ITokenProvider tokenProvidor) : IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principals = tokenProvidor.GetClaimsPrincipalsFromExpiredToken(request.ExpiredAccessToken);

        if (principals is null)
        {
            return ApplicationError.AccessTokenInvalid;
        }

        var userId = principals.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return ApplicationError.UserIdClaimInvalid;
        }

        var userResult = await identityService.GetUserByIdAsync(userId);

        var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync
        (refreshToken => refreshToken.Token == request.RefreshToken
        && refreshToken.UserId == userId, cancellationToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc <= DateTimeOffset.UtcNow)
        {
            return ApplicationError.RefreshTokkenExpired;
        }

        var generateTokenResult = await tokenProvidor.GenerateJwtTokenAsync(userResult.Value, cancellationToken);

        if (generateTokenResult.IsError)
        {
            return generateTokenResult.Errors;
        }
        return generateTokenResult.Value;
    }
}