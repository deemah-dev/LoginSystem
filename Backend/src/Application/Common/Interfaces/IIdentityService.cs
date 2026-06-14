using Application.Features.Identity.Dtos;
using Domain.Common.Results;

namespace Application.Features.Interfaces;

public interface IIdentityService
{
    Task<Result<AppUserDto>> AuthenticateAsync(string email, string password);
    Task<Result<Deleted>> DeleteUserAsync(string userId);
    Task<Result<AppUserDto>> GetUserByIdAsync(string userId);
    Task<string?> GetUserNameAsync(string userId);
    Task<Result<AppUserDto>> RegisterUserAsync(string email, string password, string username, string roleName);
}