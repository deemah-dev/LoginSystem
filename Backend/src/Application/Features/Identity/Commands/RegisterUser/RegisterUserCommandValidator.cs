using Domain.Identity;
using FluentValidation;

namespace Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Email).EmailAddress().NotEmpty().NotNull();

        RuleFor(command => command.Username).MaximumLength(10).NotEmpty().NotNull();

        RuleFor(command => command.Password)
        .NotNull()
        .NotEmpty()
        .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$");

        RuleFor(command => command.Role).IsInEnum();
    }
}