using Application.Features.Customers.Dtos;
using Application.Features.Identity;
using Domain.Common.Enums;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(string FirstName, string? MiddleName, string LastName, DateTime BirthDate, Gender Gender, string PhoneNumber, string Country, string City, string Street) : IRequest<Result<CustomerDto>>;