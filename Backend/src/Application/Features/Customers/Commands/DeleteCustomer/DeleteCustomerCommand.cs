using Domain.Common.Results;
using MediatR;

namespace Application.Features.Customers.Commands.DeleteCustomer;

public sealed record DeleteCustomerCommand(Guid? CustomerId) : IRequest<Result<Deleted>>;