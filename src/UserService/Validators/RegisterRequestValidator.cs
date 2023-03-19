using UserService.Dtos.Requests;
using FluentValidation;

namespace UserService.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty();

        RuleFor(x => x.Login)
            .EmailAddress()
            .WithMessage("This is not a valid email address");

        RuleFor(x => x.Password)
            .Length(8, 16)
            .WithMessage("Password length should be at least 8 characters, max - 16 ");
        
        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Passwords are not equal");;

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty();
    }
}