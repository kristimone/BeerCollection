using BeerCollection.Domain.Entities;
using BeerCollection.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BeerCollection.Infrastructure.Persistence
{
    public class BeerCollectionDbContext : DbContext
    {
        public BeerCollectionDbContext(DbContextOptions<BeerCollectionDbContext> options)
            : base(options) { }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<BeerRating> BeerRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BeerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BeerRatingEntityConfiguration());

            modelBuilder.Entity<Beer>().HasData(
                new Beer
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Pale Ale",
                    Type = "Ale"
                },
                new Beer
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Stout",
                    Type = "Dark"
                }
            );

            modelBuilder.Entity<BeerRating>().HasData(
                new BeerRating
                {
                    Id = 1,
                    Value = 4,
                    BeerId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                }
            );
        }
    }
}
