using AutoMapper;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerCollection.Application.Beers.Queries
{
    // Query
    public class GetBeerByIdQuery : IRequest<BeerDto>
    {
        public Guid BeerId { get; set; }
    }

    // Handler
    public class GetBeerByIdQueryHandler : IRequestHandler<GetBeerByIdQuery, BeerDto>
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBeerByIdQueryHandler> _logger;

        public GetBeerByIdQueryHandler(
            IBeerRepository beerRepository,
            IMapper mapper,
            ILogger<GetBeerByIdQueryHandler> logger)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BeerDto> Handle(GetBeerByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetBeerByIdQuery for BeerId: {BeerId}", request.BeerId);

            var beer = await _beerRepository.GetByIdAsync(request.BeerId);

            if (beer == null)
            {
                _logger.LogWarning("Beer with ID {BeerId} not found", request.BeerId);
                return null;
            }

            _logger.LogInformation("Beer with ID {BeerId} retrieved successfully", request.BeerId);
            return _mapper.Map<BeerDto>(beer);
        }
    }

    // Validator
    public class GetBeerByIdQueryValidator : AbstractValidator<GetBeerByIdQuery>
    {
        public GetBeerByIdQueryValidator()
        {
            RuleFor(x => x.BeerId)
                .NotEmpty().WithMessage("Beer ID must be provided.");
        }
    }
}
