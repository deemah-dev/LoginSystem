using FluentValidation;

namespace Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerPhoneCommandValidator : AbstractValidator<UpdateCustomerPhoneCommand>
{
    public UpdateCustomerPhoneCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .Must(id => id.HasValue && id.Value != Guid.Empty)
            .When(command => command.CustomerId.HasValue)
            .WithMessage("CustomerId must be a valid non-empty GUID.");

        RuleFor(command => command.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .WithMessage("Phone number is required.");
    }
}