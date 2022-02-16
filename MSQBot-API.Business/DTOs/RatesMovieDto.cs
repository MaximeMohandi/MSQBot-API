namespace MSQBot_API.Business.DTOs
{
    /// <summary>
    /// All the movie rates
    /// </summary>
    public class RatesMovieDto
    {
        private decimal? _rate;

        /// <summary>
        /// Movie Rated
        /// </summary>
        public MovieDto MovieRated { get; set; }

        /// <summary>
        /// User that gave the rate
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// rate given by user rouded to 2 decimal
        /// </summary>
        public decimal? Rate
        {
            get => _rate;
            set => _rate = value.HasValue ? Math.Round((decimal)value, 2) : null;
        }
    }
}