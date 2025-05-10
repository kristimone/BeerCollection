namespace BeerCollection.Domain.Entities
{
    public class BeerRating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Guid BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
