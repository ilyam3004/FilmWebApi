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
    }
}