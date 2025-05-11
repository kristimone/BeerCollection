using AutoMapper;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerCollection.Application.Beers.Queries
{
    // Query
    public class SearchBeersQuery : IRequest<IEnumerable<BeerDto>>
    {
        public string SearchTerm { get; set; }
    }

    // Handler
    public class SearchBeersQueryHandler : IRequestHandler<SearchBeersQuery, IEnumerable<BeerDto>>
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchBeersQueryHandler> _logger;


        public SearchBeersQueryHandler(IBeerRepository beerRepository, IMapper mapper, ILogger<SearchBeersQueryHandler> logger)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // Handler
        public async Task<IEnumerable<BeerDto>> Handle(SearchBeersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching beers with query: {Query}", request.SearchTerm);

            var beers = await _beerRepository.SearchByNameAsync(request.SearchTerm);
            var result = _mapper.Map<IEnumerable<BeerDto>>(beers);

            _logger.LogInformation("Found {Count} matching beers", beers.ToList().Count);

            return result;
        }
    }

    // Validator
    public class SearchBeersQueryValidator : AbstractValidator<SearchBeersQuery>
    {
        public SearchBeersQueryValidator()
        {
            RuleFor(x => x.SearchTerm)
                .NotEmpty().WithMessage("Search term is required.")
                .MinimumLength(2).WithMessage("Search term must be at least 2 characters.");
        }
    }
}
