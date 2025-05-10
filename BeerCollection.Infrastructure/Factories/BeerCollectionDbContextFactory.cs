using BeerCollection.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BeerCollection.Infrastructure.Factories
{
    public class BeerCollectionDbContextFactory : IDesignTimeDbContextFactory<BeerCollectionDbContext>
    {
        public BeerCollectionDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // or WebAPI path
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BeerCollectionDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new BeerCollectionDbContext(optionsBuilder.Options);
        }
    }
}