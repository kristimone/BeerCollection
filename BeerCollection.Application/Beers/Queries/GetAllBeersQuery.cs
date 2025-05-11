using BeerCollection.Application.Beers.DTOs;
using MediatR;
using AutoMapper;
using BeerCollection.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace BeerCollection.Application.Beers.Queries
{
    public record GetAllBeersQuery : IRequest<IEnumerable<BeerDto>>;
    // Handler
    public class GetAllBeersQueryHandler : IRequestHandler<GetAllBeersQuery, IEnumerable<BeerDto>>
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBeersQueryHandler> _logger;

        public GetAllBeersQueryHandler(IBeerRepository beerRepository, IMapper mapper, ILogger<GetAllBeersQueryHandler> logger)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BeerDto>> Handle(GetAllBeersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllBeersQuery");

            var beers = await _beerRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<BeerDto>>(beers);

            _logger.LogInformation("Retrieved {Count} beers", result.ToList().Count );

            return result;
        }
    }
}
