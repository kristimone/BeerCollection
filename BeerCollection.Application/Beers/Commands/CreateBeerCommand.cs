using AutoMapper;
using BeerCollection.Domain.Entities;
using BeerCollection.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BeerCollection.Application.Beers.Commands
{
    // Command
    public class CreateBeerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Rating { get; set; }
    }

    // Handler
    public class CreateBeerCommandHandler : IRequestHandler<CreateBeerCommand, Guid>
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBeerCommandHandler> _logger;

        public CreateBeerCommandHandler(IBeerRepository beerRepository, IMapper mapper, ILogger<CreateBeerCommandHandler> logger)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateBeerCommand for beer: {Name}, {Type}", request.Name, request.Type);
            
            var beer = _mapper.Map<Beer>(request);
            await _beerRepository.AddAsync(beer);
           
            _logger.LogInformation("Beer created with ID: {Id}", beer.Id);
           
            return beer.Id;
        }
    }

    // Validator
    public class CreateBeerCommandValidator : AbstractValidator<CreateBeerCommand>
    {
        public CreateBeerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Beer name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Beer type is required.")
                .MaximumLength(50).WithMessage("Type must be less than 50 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .When(x => x.Rating.HasValue)
                .WithMessage("Rating must be between 1 and 5.");
        }
    }
}
