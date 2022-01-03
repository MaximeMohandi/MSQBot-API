namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public class MovieDto
    {
        /// <summary>
        /// Movie unique id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// movie title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Movie poster
        /// </summary>
        public string Poster { get; set; } = string.Empty;

        /// <summary>
        /// The date when the movie has been added
        /// </summary>
        public DateTime AddedDate { get; set; }

        /// <summary>
        /// Date when the movie has been seen
        /// </summary>
        public DateTime? SeenDate { get; set; }

        /// <summary>
        /// All rates given to the movie
        /// </summary>
        public List<RateDto>? Rates { get; set; }

        /// <summary>
        /// Average rate of the movie
        /// </summary>
        public decimal? AvgRate
        {
            get => IsRatesExist() ? Math.Round(Rates.Average(r => r.Rate).Value, 2) : default(decimal?);
        }

        /// <summary>
        /// Best Rate given to the movie
        /// </summary>
        public decimal? MaxRate
        {
            get => IsRatesExist() ? Rates.Max(r => r.Rate) : default(decimal?);
        }

        /// <summary>
        /// Worst rate given to the movie
        /// </summary>
        public decimal? MinRate
        {
            get => IsRatesExist() ? Rates.Min(r => r.Rate) : default(decimal?);
        }

        private bool IsRatesExist()
        {
            return Rates is not null && Rates.Count > 0;
        }
    }
}