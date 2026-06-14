using Domain.Common.Results;

namespace Domain.Identity;

public class RefreshToken
{
    private RefreshToken(Guid id, string? token, string? userId, DateTimeOffset expiresOnUtc)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiresOnUtc = expiresOnUtc;
    }

    public Guid Id { get; }
    public string? Token { get; }
    public string? UserId { get; }
    public DateTimeOffset ExpiresOnUtc { get; }

    public static Result<RefreshToken> Create(Guid id, string? token, string? userId, DateTimeOffset expiresOnUtc)
    {
        if (id == Guid.Empty)
            return RefreshTokenErrors.IdRequired;

        if (string.IsNullOrEmpty(token))
            return RefreshTokenErrors.TokenRequired;

        if (string.IsNullOrEmpty(userId))
            return RefreshTokenErrors.UserIdRequired;

        if (expiresOnUtc <= DateTimeOffset.UtcNow)
            return RefreshTokenErrors.ExpiryInvalid;

        return new RefreshToken(id, token, userId, expiresOnUtc);
    }
}