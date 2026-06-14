using Application.Features.Identity;
using Domain.Common.Enums;
using Domain.Common.Results;
using MediatR;

namespace Application.Features.Customers.Commands.RegisterCustomerAccount;

public sealed record RegisterCustomerAccountCommand(bool CustomerExists, string? FirstName, string? MiddleName, string? LastName, DateTime? BirthDate, Gender? Gender, string PhoneNumber, string? Country, string? City, string? Street, string Email, string Username, string Password) : IRequest<Result<TokenResponse>>;