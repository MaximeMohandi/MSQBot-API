using MSQBot_API.Entities.DTOs;
using MSQBot_API.Entities.Models;

namespace MSQBot_API.Helpers
{
    public static class Mapper
    {
        /// <summary>
        /// Map list of movie entities to list of movie DTO
        /// </summary>
        /// <param name="movies">list of movie entities to map</param>
        /// <returns>list of mapped movie DTOs</returns>
        public static List<MovieDto>? MapMoviesToDTOs(this List<Movie> movies)
        {
            var result = new List<MovieDto>();

            if (movies is not null)
            {
                movies.ForEach(movie => result.Add(movie.MapMovieToDTO()));
            }

            return result;
        }

        /// <summary>
        /// Map a movie entitie to a movie DTO
        /// </summary>
        /// <param name="movie">Movie entitie to map</param>
        /// <returns>mapped movie DTO</returns>
        public static MovieDto MapMovieToDTO(this Movie movie)
        {
            return new MovieDto
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
        /// Map list of Rate entitie to a list of rate DTO
        /// </summary>
        /// <param name="rates">List of rate entities to map</param>
        /// <returns>List of mapped movie DTO</returns>
        public static List<RateDto>? MapRatesToDTOs(this List<Rate>? rates)
        {
            var result = new List<RateDto>();

            if (rates is not null)
            {
                rates.ForEach(rate =>
                {
                    result.Add(rate.MapRateToDTO());
                });
            }

            return result;
        }

        /// <summary>
        /// Map a Rate entitie to a rate DTO
        /// </summary>
        /// <param name="rate">rate entitie to map</param>
        /// <returns>mapped rate DTO</returns>
        public static RateDto? MapRateToDTO(this Rate? rate)
        {
            if (rate == null) return null;

            return new RateDto
            {
                User = rate.User.MapUserToDTO(),
                Rate = rate.Note
            };
        }

        /// <summary>
        /// Map a User entitie to a user DTO
        /// </summary>
        /// <param name="user">user entitie to map</param>
        /// <returns>mapped user DTO</returns>
        public static UserDto? MapUserToDTO(this User user)
        {
            return new UserDto { UserId = user.UserId, Name = user.Name };
        }
    }
}