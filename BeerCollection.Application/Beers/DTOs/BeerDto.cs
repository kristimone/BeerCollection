namespace BeerCollection.Application.Beers.DTOs
{
    public class BeerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double? AverageRating { get; set; }
    }
}
