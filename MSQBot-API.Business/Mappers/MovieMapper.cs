using MSQBot_API.Business.DTOs;
using MSQBot_API.Core.Entities;

namespace MSQBot_API.Business.Mappers
{
    public static class MovieMapper
    {

        /// <summary>
        /// Map a movie entitie to a movie DTO
        /// </summary>
        /// <param name="movie">Movie entitie to map</param>
        /// <returns>mapped movie DTO</returns>
        public static MovieDto MapToMovieDTO(this Movie movie)
        {
            return new MovieDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                AddedDate = movie.AddedDate,
                SeenDate = movie.SeenDate,
                Poster = movie.Poster
            };
        }

        /// <summary>
        /// Map list of movie entities to list of movie DTO
        /// </summary>
        /// <param name="movies">list of movie entities to map</param>
        /// <returns>list of mapped movie DTOs</returns>
        public static List<MovieDto> MapToMovieDTOs(this List<Movie> movies)
        {
            var result = new List<MovieDto>();
            movies.ForEach(movie => result.Add(movie.MapToMovieDTO()));

            return result;
        }

        /// <summary>
        /// Map a movie entitie to a detailled movie DTO
        /// </summary>
        /// <param name="movie">Movie entitie to map</param>
        /// <returns>mapped movie detailled DTO</returns>
        public static MovieDetailsDto MapToMovieDetailsDTO(this Movie movie)
        {
            return new MovieDetailsDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                AddedDate = movie.AddedDate,
                SeenDate = movie.SeenDate,
                Rates = movie.Rates.MapRatesToDTOs(),
                Poster = movie.Poster
            };
        }

        /// <summary>
        /// Map list of movie entities to list of movie detailled DTO
        /// </summary>
        /// <param name="movies">list of movie entities to map</param>
        /// <returns>list of mapped movie detailled DTOs</returns>
        public static List<MovieDetailsDto>? MapToMovieDetailsDTOs(this List<Movie> movies)
        {
            if (movies == null) throw new ArgumentNullException(nameof(movies));

            var result = new List<MovieDetailsDto>();
            movies.ForEach(movie => result.Add(movie.MapToMovieDetailsDTO()));

            return result;
        }
    }
}
