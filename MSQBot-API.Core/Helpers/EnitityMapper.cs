
namespace MSQBot_API.Core.Helpers
{
    /// <summary>
    /// Map Entities to DTOs
    /// </summary>
    public static class EnitityMapper
    {
        #region MovieDTO mapper

        /// <summary>
        /// Map a movie entitie to a movie DTO
        /// </summary>
        /// <param name="movie">Movie entitie to map</param>
        /// <returns>mapped movie DTO</returns>
        public static MovieDto MapToMovieDTO(this Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));
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
            if (movies == null) throw new ArgumentNullException(nameof(movies));

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
            if (movie == null) throw new ArgumentNullException(nameof(movie));

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

        #endregion MovieDTO mapper

        #region RateDTO mapper

        /// <summary>
        /// Map list of Rate entitie to a list of rate DTO
        /// </summary>
        /// <param name="rates">List of rate entities to map</param>
        /// <returns>List of mapped movie DTO</returns>
        public static List<RatesMovieDto>? MapRatesToDTOs(this List<Rate>? rates)
        {
            var result = new List<RatesMovieDto>();

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
        public static RatesMovieDto? MapRateToDTO(this Rate? rate)
        {
            if (rate == null) return null;

            return new RatesMovieDto
            {
                User = rate.User.MapUserToDTO(),
                Rate = rate.Note,
                MovieRated = rate.Movie.MapToMovieDTO()
            };
        }

        #endregion RateDTO mapper

        #region UserDTO mapper

        /// <summary>
        /// Map a User entitie to a user DTO
        /// </summary>
        /// <param name="user">user entitie to map</param>
        /// <returns>mapped user DTO</returns>
        public static UserDto? MapUserToDTO(this User user)
        {
            return new UserDto { UserId = user.UserId, Name = user.Name };
        }

        #endregion UserDTO mapper
    }
}