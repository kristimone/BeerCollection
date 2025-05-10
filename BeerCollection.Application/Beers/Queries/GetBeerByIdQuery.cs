using AutoMapper;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;

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

        public GetBeerByIdQueryHandler(IBeerRepository beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<BeerDto> Handle(GetBeerByIdQuery request, CancellationToken cancellationToken)
        {
            var beer = await _beerRepository.GetByIdAsync(request.BeerId);

            if (beer == null)
                return null;

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
