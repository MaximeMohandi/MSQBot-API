using MSQBot_API.Core.DTOs.Users;
using MSQBot_API.Core.Interfaces.Movies;

namespace MSQBot_API.Core.DTOs.Movies
{
    /// <summary>
    /// All the movie rates
    /// </summary>
    public record RatesMovieDto : IRate
    {

        /// <summary>
        /// Movie Rated
        /// </summary>
        public MovieDto Movie { get; init; }

        /// <summary>
        /// User that gave the rate
        /// </summary>
        public UserDto User { get; init; }

        /// <summary>
        /// rate given by user rouded to 2 decimal
        /// </summary>
        public decimal? Rate { get; init; }
    }
}