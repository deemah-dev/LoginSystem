using Application.Features.Errors;
using Application.Features.Interfaces;
using Domain.Common.Results;
using Domain.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerPhoneCommandHandler(IUser user,
                                                        IAppDbContext context)
                                                        : IRequestHandler<UpdateCustomerPhoneCommand, Result<Updated>>
{
    public async Task<Result<Updated>> Handle(UpdateCustomerPhoneCommand request, CancellationToken cancellationToken)
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

        customer.UpdatePhone(request.PhoneNumber);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}