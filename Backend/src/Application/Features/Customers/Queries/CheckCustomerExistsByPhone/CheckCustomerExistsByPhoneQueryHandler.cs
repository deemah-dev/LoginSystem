using Application.Features.Interfaces;
using Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Queries.CheckCustomerExistsByPhone;

public sealed class CheckCustomerExistsByPhoneQueryHandler(IAppDbContext context)
: IRequestHandler<CheckCustomerExistsByPhoneQuery, bool>
{
    public async Task<bool> Handle(CheckCustomerExistsByPhoneQuery request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (customer is null)
        {
            return false;
        }

        return true;
    }
}