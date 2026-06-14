using FluentValidation;

namespace Application.Features.Customers.Commands.RegisterCustomerAccount;

public class RegisterCustomerAccountCommandValidator : AbstractValidator<RegisterCustomerAccountCommand>
{
    public RegisterCustomerAccountCommandValidator()
    {
        RuleFor(command => command.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required.");

        RuleFor(command => command.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email address is required.");

        RuleFor(command => command.Username)
            .NotNull()
            .NotEmpty()
            .MaximumLength(10)
            .WithMessage("Username is required and must be 10 characters or fewer.");

        RuleFor(command => command.Password)
            .NotNull()
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, and one digit.");

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .When(command => command.FirstName is not null)
            .WithMessage("First name cannot be empty when provided.");

        RuleFor(command => command.LastName)
            .NotEmpty()
            .When(command => command.LastName is not null)
            .WithMessage("Last name cannot be empty when provided.");

        RuleFor(command => command.BirthDate)
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
            .When(command => command.BirthDate.HasValue)
            .WithMessage("Customer must be at least 18 years old.");

        RuleFor(command => command.Gender)
            .IsInEnum()
            .When(command => command.Gender.HasValue)
            .WithMessage("Gender is invalid.");

        RuleFor(command => command.Country)
            .NotEmpty()
            .When(command => command.Country is not null)
            .WithMessage("Country cannot be empty when provided.");

        RuleFor(command => command.City)
            .NotEmpty()
            .When(command => command.City is not null)
            .WithMessage("City cannot be empty when provided.");

        RuleFor(command => command.Street)
            .NotEmpty()
            .When(command => command.Street is not null)
            .WithMessage("Street cannot be empty when provided.");


        RuleFor(command => command.FirstName)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("First name is required.");

        RuleFor(command => command.LastName)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("Last name is required.");

        RuleFor(command => command.BirthDate)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("Birth date is required");

        RuleFor(command => command.Gender)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("Gender is required.");

        RuleFor(command => command.Country)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("Country is required.");

        RuleFor(command => command.City)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("City is required.");

        RuleFor(command => command.Street)
            .NotNull()
            .When(command => !command.CustomerExists)
            .WithMessage("Street is required.");
    }
}