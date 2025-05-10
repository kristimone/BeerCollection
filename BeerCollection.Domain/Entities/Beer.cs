namespace BeerCollection.Domain.Entities
{
    public class Beer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Type { get; set; }

        public List<BeerRating> Ratings { get; set; } = new();

        public double? AverageRating =>
            Ratings != null && Ratings.Count > 0
                ? Math.Round((double)Ratings.Sum(r => r.Value) / Ratings.Count, 2)
                : null;
    }
}