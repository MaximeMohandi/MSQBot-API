namespace MSQBot_API.Core.DTOs
{
    /// <summary>
    /// All the movie rates
    /// </summary>
    public class RatesMovieDto : RateDtoBase
    {

        /// <summary>
        /// Movie Rated
        /// </summary>
        public MovieDto MovieRated { get; set; }

        /// <summary>
        /// User that gave the rate
        /// </summary>
        public UserDto User { get; set; }
    }
}