using BeerCollection.Application.Beers.Commands;
using BeerCollection.Application.Beers.DTOs;
using BeerCollection.Application.Beers.Queries;
using BeerCollection.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BeerCollection.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BeerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody] CreateBeerCommand command)
        {
            var beerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllBeers), new { id = beerId }, new { id = beerId });
        }

        [HttpPut("{id}/rating")]
        public async Task<IActionResult> UpdateRating(Guid id, [FromBody] int rating)
        {
            var command = new UpdateBeerRatingCommand
            {
                BeerId = id,
                NewRating = rating
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetAllBeers()
        {
            var beers = await _mediator.Send(new GetAllBeersQuery());
            return Ok(beers);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Beer>>> SearchBeers([FromQuery] string term)
        {
            var query = new SearchBeersQuery { SearchTerm = term };
            var beers = await _mediator.Send(query);
            return Ok(beers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeerById(Guid id)
        {
            var beer = await _mediator.Send(new GetBeerByIdQuery { BeerId = id });
            if (beer == null)
                return NotFound();

            return Ok(beer);
        }
    }
}
