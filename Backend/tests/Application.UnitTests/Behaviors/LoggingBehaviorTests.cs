using Application.Common.Behaviors;
using Application.Features.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Behaviors;

public class LoggingBehaviorTests
{
    public class DummyRequest;
    private readonly ILogger<DummyRequest> logger;
    private readonly IUser user;
    private readonly IIdentityService identityService;

    private readonly LoggingBehavior<DummyRequest> behavior;
    public LoggingBehaviorTests()
    {
        logger = Substitute.For<ILogger<DummyRequest>>();
        user = Substitute.For<IUser>();
        identityService = Substitute.For<IIdentityService>();

        behavior = new LoggingBehavior<DummyRequest>(logger, user, identityService);
    }

    [Fact]
    async Task Process_WithUserId_LogsRequestWithUserName()
    {
        var request = new DummyRequest();
        user.Id.Returns("DummyUserId");
        identityService.GetUserNameAsync("DummyUserId").Returns("DummyUserName");

        await behavior.Process(request, CancellationToken.None);

        await identityService.Received(1).GetUserNameAsync("DummyUserId");
        logger.Received(1).Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Request")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }
    [Fact]
    async Task Process_WithoutUserId_LogRequestWithEmptyUserName()
    {
        var request = new DummyRequest();
        user.Id.Returns((string?)null);

        await behavior.Process(request, CancellationToken.None);

        await identityService.DidNotReceive().GetUserNameAsync(Arg.Any<string>());
        logger.Received(1).Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains("Request")),
            Arg.Any<Exception>(),
            Arg.Any<Func<object, Exception?, string>>());
    }
}