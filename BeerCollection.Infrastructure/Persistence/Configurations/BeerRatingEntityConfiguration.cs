using BeerCollection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BeerCollection.Infrastructure.Persistence.Configurations
{
    public class BeerRatingEntityConfiguration : IEntityTypeConfiguration<BeerRating>
    {
        public void Configure(EntityTypeBuilder<BeerRating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Value)
                .IsRequired();

            builder.HasOne(r => r.Beer)
                .WithMany(b => b.Ratings)
                .HasForeignKey(r => r.BeerId);
        }
    }
}
