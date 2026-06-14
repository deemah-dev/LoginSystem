using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Commands.RevokeToken;

public sealed record RevokeTokenCommand(string UserId) : IRequest<Result<Success>>;