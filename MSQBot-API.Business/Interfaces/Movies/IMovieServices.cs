using MSQBot_API.Core.DTOs.Movies;
using MSQBot_API.Core.Interfaces.Movies;

namespace MSQBot_API.Business.Interfaces.Movies
{
    public interface IMovieServices : IBusinessServices<MovieRatedDto, MovieCreationDto>
    {
        /// <summary>
        /// Get a view with all movies and key data
        /// </summary>
        /// <returns>A view with key datas</returns>
        Task<MoviesViewDto> GetGlobalView();

        /// <summary>
        /// Update seen date of movie if it's not already set.
        /// </summary>
        /// <param name="movieId">id movie to update</param>
        Task UpdateSeenDate(int movieId);

        /// <summary>
        /// Replace the title of a movie.
        /// </summary>
        /// <param name="newMovieTitle">DTO representing the change</param>
        Task UpdateName(MovieTitleUpdateDto newMovieTitle);

        /// <summary>
        /// Get a wallpaper of the movie
        /// </summary>
        /// <param name="movie">movie title</param>
        /// <returns>link to a wallpaper image</returns>
        Task<string> GetMovieWallpaper(string title);

        /// <summary>
        /// Change the poster for all the movies in database
        /// </summary>
        Task UpdateAllMoviePoster();

        /// <summary>
        /// Get a random movie to watch
        /// </summary>
        Task<IMovie> GetRandomMovie();
    }
}
