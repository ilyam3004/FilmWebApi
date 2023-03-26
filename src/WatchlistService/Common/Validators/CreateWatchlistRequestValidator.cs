using WatchlistService.Dtos.Requests;
using FluentValidation;

namespace WatchlistService.Common.Validators;

public class CreateWatchlistRequestValidator : 
    AbstractValidator<CreateWatchlistRequest>
{
    public CreateWatchlistRequestValidator()
    {
        RuleFor(w => w.WatchlistName)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(w => w.Token)
            .NotEmpty()
            .WithMessage("Token is required for authentication");
    }
}