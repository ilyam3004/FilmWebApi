using FluentValidation;
using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Validators;

public class AddMovieRequestValidator : 
    AbstractValidator<AddMovieRequest>
{
    public AddMovieRequestValidator()
    {
        RuleFor(w => w.WatchlistId)
            .NotEmpty()
            .WithMessage("WatchlistId is required");
        
        RuleFor(w => w.MovieId)
            .NotEmpty()
            .WithMessage("MovieId is required");    
    }
}