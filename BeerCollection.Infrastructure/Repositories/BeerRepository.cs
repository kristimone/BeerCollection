using BeerCollection.Domain.Entities;
using BeerCollection.Domain.Interfaces;
using BeerCollection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BeerCollection.Infrastructure.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerCollectionDbContext _context;

        public BeerRepository(BeerCollectionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Beer entity)
        {
            await _context.Beers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddRatingAsync(Guid beerId, int rating)
        {
            var beer = await _context.Beers
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == beerId);

            if (beer == null)
                return false;

            beer.Ratings.Add(new BeerRating
            {
                Value = rating,
                BeerId = beer.Id
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            return await _context.Beers.Include(b => b.Ratings).ToListAsync();
        }
        public async Task<Beer?> GetByIdAsync(Guid beerId)
        {
            return await _context.Beers
                .Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Id == beerId);
        }

        public async Task<Beer> GetByNameAsync(string name)
        {
            return await _context.Beers.Include(b => b.Ratings)
                .FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<IEnumerable<Beer>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Beers
                .Include(b => b.Ratings)
                .Where(b => b.Name.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
