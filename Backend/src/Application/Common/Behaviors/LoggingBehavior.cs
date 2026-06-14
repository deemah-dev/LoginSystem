using Application.Features.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class LoggingBehavior<TRequest>
(ILogger<TRequest> logger,
 IUser user,
  IIdentityService identityService)
   : IRequestPreProcessor<TRequest>
   where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var userId = user.Id ?? string.Empty;
        string? userName = string.Empty;
        if (!string.IsNullOrWhiteSpace(userId))
            userName = await identityService.GetUserNameAsync(userId);
        logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}", typeof(TRequest).Name, user.Id, userName, request);
    }
}