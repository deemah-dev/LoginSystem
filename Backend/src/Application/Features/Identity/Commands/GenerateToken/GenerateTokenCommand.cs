using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Queries.GenerateToken;

public sealed record GenerateTokenCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;