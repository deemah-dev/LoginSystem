using Application.Features.Customers.Dtos;
using Domain.Common.Results;

namespace Application.Features.Errors;

public static class ApplicationError
{
    public static readonly Error AccessTokenInvalid = Error.Conflict(
        code: "Auth.ExpiredAccessToken.Invalid",
        description: "Expired access token is not valid.");
    public static readonly Error UserIdClaimInvalid = Error.Conflict(
        code: "Auth.UserIdClaim.Invalid",
        description: "Invalid userId claim.");
    public static readonly Error RefreshTokkenExpired = Error.Conflict(
        code: "Auth.RefreshToken.Expired",
        description: "Refresh token is invalid or has expired.");
    internal static readonly Error CustomerNotFound = Error.NotFound(
        "Customer.UserId.NotFound",
        "Cannot find customer by userId.");
    internal static readonly Error CustomerAlreadyHaveAccount = Error.Conflict(
        "Customer.Account.Exists",
        "Cannot create new account for customer, customer already have an account"
    );

    public static readonly Error CustomerAlreadyExists = Error.Conflict(
        "Customer.PhoneNumber.Exists",
        "Phone Number Already Used"
    );
}