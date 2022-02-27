using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;

namespace MSQBot_API.Core.Interfaces
{
    public interface IMovieServices : IBusinessServices<MovieDto, MovieCreationDto>
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
    }
}
