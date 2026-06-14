using Application.Features.Errors;
using Application.Features.Interfaces;
using Application.Features.Identity;
using Domain.Common.Results;
using Domain.Customers;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Commands.RegisterCustomerAccount;

public sealed class RegisterCustomerAccountCommandHandler
(IAppDbContext context,
IIdentityService identityService,
ITokenProvider tokenProvider) : IRequestHandler<RegisterCustomerAccountCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(RegisterCustomerAccountCommand request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (customer is null)
        {
            var createCustomerResult = Customer.Create(request.FirstName!, request.MiddleName, request.LastName!,
                                                        request.BirthDate!.Value, request.Gender!.Value,
                                                        request.PhoneNumber, request.Country!, request.City!,
                                                        request.Street!);

            if (createCustomerResult.IsError)
            {
                return createCustomerResult.Errors;
            }
            customer = createCustomerResult.Value;
            await context.Customers.AddAsync(customer, cancellationToken);
        }

        if (customer.UserId is not null)
        {
            return ApplicationError.CustomerAlreadyHaveAccount;
        }

        var createUserResult = await identityService.RegisterUserAsync(request.Email, request.Password, request.Username, Role.Customer.ToString());

        if (createUserResult.IsError)
        {
            return createUserResult.Errors;
        }

        customer.LinkToAccount(createUserResult.Value.UserId);
        await context.SaveChangesAsync(cancellationToken);

        var user = createUserResult.Value;

        var generateTokenResult = await tokenProvider.GenerateJwtTokenAsync(user, cancellationToken);

        if (generateTokenResult.IsError)
        {
            return generateTokenResult.Errors;
        }

        return generateTokenResult.Value;

    }
}