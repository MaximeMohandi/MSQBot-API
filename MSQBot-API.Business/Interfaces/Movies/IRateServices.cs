using MSQBot_API.Core.DTOs.Movies;

namespace MSQBot_API.Business.Interfaces.Movies
{
    /// <summary>
    /// Rate services methods
    /// </summary>
    public interface IRateServices
    {
        /// <summary>
        /// Get all the rate a user had given to a movie.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of rates with movie associated for a user</returns>
        public Task<UserMovieRateDto> GetRatesUser(long userId);

        /// <summary>
        /// Add a new rate to a movie in database
        /// </summary>
        /// <param name="movieRated">data used to rate a movie</param>
        public Task RateMovie(MovieRateCreationDto movieRate, IMovieServices movieServices);
    }
}
