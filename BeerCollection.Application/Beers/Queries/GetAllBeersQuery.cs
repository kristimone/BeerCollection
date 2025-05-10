using BeerCollection.Application.Beers.DTOs;
using MediatR;
using AutoMapper;
using BeerCollection.Domain.Interfaces;

namespace BeerCollection.Application.Beers.Queries
{
    public record GetAllBeersQuery : IRequest<IEnumerable<BeerDto>>;
    // Handler
    public class GetAllBeersQueryHandler : IRequestHandler<GetAllBeersQuery, IEnumerable<BeerDto>>
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;

        public GetAllBeersQueryHandler(IBeerRepository beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerDto>> Handle(GetAllBeersQuery request, CancellationToken cancellationToken)
        {
            var beers = await _beerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BeerDto>>(beers);
        }
    }
}
