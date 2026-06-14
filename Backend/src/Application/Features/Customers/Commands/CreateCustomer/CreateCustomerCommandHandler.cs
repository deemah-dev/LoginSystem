using Application.Features.Errors;
using Application.Features.Interfaces;
using Application.Features.Customers.Dtos;
using Application.Features.Customers.Mappers;
using Application.Features.Identity;
using Domain.Common.Results;
using Domain.Customers;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler
(IAppDbContext context)
: IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request,
                                            CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (customer is not null)
        {
            return ApplicationError.CustomerAlreadyExists;
        }

        var createCustomerResult =
            Customer.Create(
                request.FirstName, request.MiddleName, request.LastName,
                request.BirthDate, request.Gender, request.PhoneNumber,
                request.Country, request.City, request.Street);

        if (createCustomerResult.IsError)
        {
            return createCustomerResult.Errors;
        }

        customer = createCustomerResult.Value;

        context.Customers.Add(customer);
        await context.SaveChangesAsync(cancellationToken);

        return customer.ToDto();
    }
}