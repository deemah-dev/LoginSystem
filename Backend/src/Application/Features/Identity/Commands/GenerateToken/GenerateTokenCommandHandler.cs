using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Queries.GenerateToken;

public class GenerateTokenCommandHandler(IIdentityService identityService, ITokenProvider tokenProvidor)
: IRequestHandler<GenerateTokenCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var userResponseResult = await identityService.AuthenticateAsync(request.Email, request.Password);

        if (userResponseResult.IsError)
            return userResponseResult.Errors;

        var generateTokenResult = await tokenProvidor.GenerateJwtTokenAsync(userResponseResult.Value, cancellationToken);

        if (generateTokenResult.IsError)
            return generateTokenResult.Errors;

        return generateTokenResult.Value;
    }
}