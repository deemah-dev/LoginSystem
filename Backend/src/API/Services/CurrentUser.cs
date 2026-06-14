using System.Security.Claims;

using Application.Features.Interfaces;

namespace API.Services;

public class CurrentUser(IHttpContextAccessor http) : IUser
{
    public string? Id => http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}