using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;

namespace MSQBot_API.Core.Interfaces
{
    public interface IMovieServices
    {
        /// <summary>
        /// Fetch details on a movie
        /// </summary>
        /// <param name="movieId">id movie to fetch</param>
        /// <returns>A movie with all it's rates</returns>
        Task<MovieRatedDto> GetMovie(int movieId);

        /// <summary>
        /// Fetch all movies
        /// </summary>
        /// <returns></returns>
        Task<List<MovieDto>> GetMovies();


        /// <summary>
        /// Add a new movie in database
        /// </summary>
        /// <param name="movie">the new movie to insert</param>
        Task AddMovie(MovieCreationDto movie);

        /// <summary>
        /// Update seen date of movie if it's not already set.
        /// </summary>
        /// <param name="movieId">id movie to update</param>
        Task UpdateMovieSeenDate(int movieId);

        /// <summary>
        /// Replace the title of a movie.
        /// </summary>
        /// <param name="newMovieTitle">DTO representing the change</param>
        Task UpdateMovieName(MovieTitleUpdateDto newMovieTitle);
    }
}
