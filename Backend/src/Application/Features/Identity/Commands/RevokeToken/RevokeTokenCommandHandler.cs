using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Commands.RevokeToken;

public class RevokeTokenCommandHandler(ITokenProvider tokenProvider) : IRequestHandler<RevokeTokenCommand, Result<Success>>
{
    public async Task<Result<Success>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var revokeTokenResult = await tokenProvider.RevokeTokenAsync(request.UserId, cancellationToken);

        if (revokeTokenResult.IsError)
            return revokeTokenResult.Errors;

        return Result.Success;
    }
}