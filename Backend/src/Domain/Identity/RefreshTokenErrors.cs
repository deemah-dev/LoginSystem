using Domain.Common.Results;

namespace Domain.Identity;

public static class RefreshTokenErrors
{
    public static readonly Error IdRequired =
        Error.Validation("RefreshToken_Id_Required", "Refresh token Id is required");

    public static readonly Error TokenRequired =
        Error.Validation("RefreshToken_Token_Required", "Token value is required.");

    public static readonly Error UserIdRequired =
        Error.Validation("RefreshToken_UserId_Required", "User Id is required.");

    public static readonly Error ExpiryInvalid =
        Error.Validation("RefreshToken_Expiry_Invalid", "Expiry must be in the future.");
}