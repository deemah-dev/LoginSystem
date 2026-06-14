using Application.Features.Identity.Dtos;
using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserCommandHandler
(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, Result<Created>>
{
    public async Task<Result<Created>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerUserResult = await identityService.RegisterUserAsync(request.Email, request.Password, request.Username, request.Role.ToString());

        if (registerUserResult.IsError)
            return registerUserResult.Errors;

        return Result.Created;
    }
}