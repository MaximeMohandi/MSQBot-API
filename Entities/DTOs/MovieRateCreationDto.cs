namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Data used to rate a movie
    /// </summary>
    public class MovieRateCreationDto
    {
        private decimal _rate;

        /// <summary>
        /// Id of the movie rated
        /// </summary>
        public int MoviId { get; set; }

        /// <summary>
        /// Id of the user who rate the movie
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Rate given by the user to the movie (rounded to two decimal)
        /// </summary>
        public decimal Rate { get => _rate; set => _rate = Math.Round(value, 2); }
    }
}