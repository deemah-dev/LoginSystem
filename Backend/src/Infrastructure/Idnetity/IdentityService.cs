using Application.Features.Identity.Dtos;
using Application.Features.Interfaces;
using Domain.Common.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Idnetity;

public class IdentityService
    (UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IIdentityService
{
    public async Task<Result<AppUserDto>> AuthenticateAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Error.NotFound("Auth.Email.NotFound", "User Not Found");
        }

        // if (!user.EmailConfirmed)
        // {
        //     throw new Exception("Email Not Found");
        // }

        if (!await userManager.CheckPasswordAsync(user, password))
        {
            return Error.Failure("Auth.Password.Invalid", "Wrong Login Attemps");
        }

        return new AppUserDto(user.Id, user.Email!, await userManager.GetRolesAsync(user));
    }

    public Task<Result<Deleted>> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AppUserDto>> GetUserByIdAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new Exception("User Not Found");
        }

        return new AppUserDto(user.Id, user.Email!, await userManager.GetRolesAsync(user));
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<Result<AppUserDto>> RegisterUserAsync(string email, string password, string username, string roleName)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            UserName = username
        };

        var result = await userManager.CreateAsync(user, password);


        if (result.Succeeded)
        {
            var addToRoleSucceeded = await AddUserToRoleAsync(user, roleName);
            if (addToRoleSucceeded.IsSuccess)
                return new AppUserDto(user.Id, user.Email!, await userManager.GetRolesAsync(user));

        }
        var errors = result.Errors.Select(err => err.Description);
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }

        return Error.Failure();
    }

    private async Task<Result<Success>> AddUserToRoleAsync(AppUser user, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var role = new AppRole
            {
                Name = roleName
            };
            await roleManager.CreateAsync(role);
        }

        var addToRole = await userManager.AddToRoleAsync(user, roleName);

        if (addToRole.Errors.Count() > 0)
            return Error.Failure();

        return Result.Success;
    }
}