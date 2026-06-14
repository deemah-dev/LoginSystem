using Application.Features.Errors;
using Application.Features.Interfaces;
using Domain.Common.Results;
using Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(IAppDbContext context,
IIdentityService identityService, IUser user) : IRequestHandler<DeleteCustomerCommand, Result<Deleted>>
{
    public async Task<Result<Deleted>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer;

        if (request.CustomerId is not null)
        {
            customer = await
        context.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId
        , cancellationToken);
        }
        else
        {
            customer = await
        context.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id
        , cancellationToken);
        }

        if (customer is null)
        {
            return ApplicationError.CustomerNotFound;
        }

        customer.Delete();

        var deleteUserResult = await identityService.DeleteUserAsync(user.Id!);

        if (deleteUserResult.IsError)
        {
            return deleteUserResult.Errors;
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}