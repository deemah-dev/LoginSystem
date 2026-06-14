using Domain.Common.Results;
using MediatR;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerPhoneCommand(Guid? CustomerId, string PhoneNumber) : IRequest<Result<Updated>>;