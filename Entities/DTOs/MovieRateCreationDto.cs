namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Data used to rate a movie
    /// </summary>
    public class MovieRateCreationDto
    {
        public int MoviId { get; set; }
        public long UserId { get; set; }
        public decimal Rate { get; set; }
    }
}