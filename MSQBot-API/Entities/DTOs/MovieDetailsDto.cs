namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Front representation of a movie
    /// </summary>
    public class MovieDetailsDto : MovieDto
    {
        /// <summary>
        /// All rates given to the movie
        /// </summary>
        public List<RatesMovieDto>? Rates { get; set; }

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