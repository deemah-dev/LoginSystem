using Domain.Common.Results;
using MediatR;

namespace Application.Features.Customers.Queries.CheckCustomerExistsByPhone;

public sealed record CheckCustomerExistsByPhoneQuery(string PhoneNumber) : IRequest<bool>;