using BeerCollection.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BeerCollection.Infrastructure.Persistence.Configurations
{
    public class BeerEntityConfiguration : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(b => b.Ratings)
                .WithOne(r => r.Beer)
                .HasForeignKey(r => r.BeerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
