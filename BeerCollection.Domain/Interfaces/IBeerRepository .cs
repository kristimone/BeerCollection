using BeerCollection.Domain.Entities;

namespace BeerCollection.Domain.Interfaces
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<bool> AddRatingAsync(Guid beerId, int rating);
        Task<IEnumerable<Beer>> SearchByNameAsync(string searchTerm);
    }
}
