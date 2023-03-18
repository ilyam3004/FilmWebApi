using UserService.Dtos.Requests;
using FluentValidation;

namespace UserService.Validators;

public class RegisterRequestValidator :AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.Password)
            .Equal(x => x.ConfirmPassword);           
    }
}