using AutoMapper;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;

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

        public SearchBeersQueryHandler(IBeerRepository beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerDto>> Handle(SearchBeersQuery request, CancellationToken cancellationToken)
        {
            var beers = await _beerRepository.SearchByNameAsync(request.SearchTerm);
            return _mapper.Map<IEnumerable<BeerDto>>(beers);
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
