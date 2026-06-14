using System.Security.Claims;
using Application.Features.Identity.Dtos;
using Application.Features.Identity;
using Domain.Common.Results;

namespace Application.Features.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken cancellationToken = default);

    ClaimsPrincipal? GetClaimsPrincipalsFromExpiredToken(string token);

    Task<Result<Success>> RevokeTokenAsync(string userId, CancellationToken ct);
}