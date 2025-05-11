using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<UpdateBeerRatingCommandHandler> _logger;

        public UpdateBeerRatingCommandHandler(IBeerRepository beerRepository, ILogger<UpdateBeerRatingCommandHandler> logger)
        {
            _beerRepository = beerRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateBeerRatingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating rating for beer ID: {BeerId} with rating: {Rating}", request.BeerId,
                request.NewRating);

            var beer = await _beerRepository.GetByIdAsync(request.BeerId);
            if (beer == null)
            {
                _logger.LogWarning("Beer not found with ID: {BeerId}", request.BeerId);
                return false;
            }

            var success = await _beerRepository.AddRatingAsync(request.BeerId, request.NewRating);

            _logger.LogInformation("Updated rating for beer ID: {BeerId}", beer.Id);
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