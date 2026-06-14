using Application.Features.Identity.Dtos;
using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Identity.Queries.GetUserInfo;

public class GetUserByIdQueryHandler(IIdentityService identityService)
: IRequestHandler<GetUserByIdQuery, Result<AppUserDto>>
{
    public async Task<Result<AppUserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await identityService.GetUserByIdAsync(request.UserId!);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }
        return userResult.Value;
    }
}