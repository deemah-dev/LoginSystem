using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Queries.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken, string ExpiredAccessToken)
: IRequest<Result<TokenResponse>>;