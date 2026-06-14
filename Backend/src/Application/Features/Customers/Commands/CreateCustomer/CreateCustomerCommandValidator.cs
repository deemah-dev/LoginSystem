using FluentValidation;
using Domain.Common.Enums;

namespace Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(command => command.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(command => command.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(command => command.BirthDate)
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
            .WithMessage("Customer must be at least 18 years old.");

        RuleFor(command => command.Gender)
            .IsInEnum()
            .WithMessage("Gender is invalid.");

        RuleFor(command => command.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required.");

        RuleFor(command => command.Country)
            .NotNull()
            .NotEmpty()
            .WithMessage("Country is required.");

        RuleFor(command => command.City)
            .NotNull()
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(command => command.Street)
            .NotNull()
            .NotEmpty()
            .WithMessage("Street is required.");
    }
}