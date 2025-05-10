using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BeerCollection.Application.Beers.Commands
{
    // Command
    public class UpdateBeerRatingCommand : IRequest<bool>
    {
        public Guid BeerId { get; set; }
        public int NewRating { get; set; }
    }

    // Handler
    public class UpdateBeerRatingCommandHandler : IRequestHandler<UpdateBeerRatingCommand, bool>
    {
        private readonly IBeerRepository _beerRepository;

        public UpdateBeerRatingCommandHandler(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<bool> Handle(UpdateBeerRatingCommand request, CancellationToken cancellationToken)
        {
            var success = await _beerRepository.AddRatingAsync(request.BeerId, request.NewRating);
            return success;
        }
    }

    // Validator
    public class UpdateBeerRatingCommandValidator : AbstractValidator<UpdateBeerRatingCommand>
    {
        public UpdateBeerRatingCommandValidator()
        {
            RuleFor(x => x.BeerId)
                .NotEmpty().WithMessage("Beer ID must be provided.");

            RuleFor(x => x.NewRating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");
        }
    }
}
