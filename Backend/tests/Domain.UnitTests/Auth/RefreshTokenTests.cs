using Domain.Identity;
using Tests.Common.Auth;
using Xunit;

namespace Domain.UnitTests.Auth;

public class RefreshTokenTests
{
    [Fact]
    public void CreateRefreshToken_ShouldSucceed_WithValidData()
    {
        var result = RefreshTokenFactory.CreateRefreshToken();

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);

        var refreshToken = result.Value;

        Assert.NotNull(refreshToken);
        Assert.False(string.IsNullOrWhiteSpace(refreshToken.UserId));
        Assert.True(refreshToken.ExpiresOnUtc > DateTimeOffset.UtcNow);
    }

    [Fact]
    public void CreateRefreshToken_ShouldFail_WhenIdEmpty()
    {
        var result = RefreshTokenFactory.CreateRefreshToken(id: Guid.Empty);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(result.TopError.Code, RefreshTokenErrors.IdRequired.Code);
    }

    [Theory]
    [InlineData("")]
    public void CreateRefreshToken_ShouldFail_WhenTokenInvalid(string? invalidToken)
    {
        var result = RefreshTokenFactory.CreateRefreshToken(token: invalidToken);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(RefreshTokenErrors.TokenRequired.Code, result.TopError.Code);
    }

    [Theory]
    [InlineData("")]
    public void CreateRefreshToken_ShouldFail_WhenUserIdInvalid(string? invalidUserId)
    {
        var result = RefreshTokenFactory.CreateRefreshToken(userId: invalidUserId);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(RefreshTokenErrors.UserIdRequired.Code, result.TopError.Code);
    }

    [Fact]
    public void CreateRefreshToken_ShouldFail_WhenExpiresOnUtcIsInPast()
    {
        var result = RefreshTokenFactory.CreateRefreshToken(expiresOnUtc: DateTimeOffset.UtcNow.AddMinutes(-1));

        Assert.True(result.IsError);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(RefreshTokenErrors.ExpiryInvalid.Code, result.TopError.Code);
    }
}