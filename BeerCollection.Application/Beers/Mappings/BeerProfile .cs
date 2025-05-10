using AutoMapper;
using BeerCollection.Application.Beers.Commands;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Domain.Entities;

namespace BeerCollection.Application.Beers.Mappings
{
    public class BeerProfile : Profile
    {
        public BeerProfile()
        {
            CreateMap<Beer, BeerDto>();
            CreateMap<CreateBeerCommand, Beer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src =>
                    src.Rating.HasValue
                        ? new List<BeerRating> { new BeerRating { Value = src.Rating.Value } }
                        : new List<BeerRating>()));
            CreateMap<UpdateBeerRatingCommand, BeerRating>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.NewRating))
                .ForMember(dest => dest.BeerId, opt => opt.MapFrom(src => src.BeerId));
        }
    }
}
