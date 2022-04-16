
using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Extension;

namespace MSQBot_API.Core.Helpers
{
    /// <summary>
    /// Map Entities to DTOs
    /// </summary>
    public static class EntityMapper
    {
        #region MovieDTO mapper

        public static MoviesViewDto MapToViewDto(this List<Movie> movies)
        {
            List<MovieRatedDto> ratedMovies = movies.MapToDTO();
            return new()
            {
                Movies = ratedMovies,
                SeenMovieCount = ratedMovies.Count(m => m.SeenDate is not null),
                ToSeeMovieCount = ratedMovies.Count(m => m.SeenDate is null),
                BestMovie = ratedMovies.OrderByDescending(m => m.AvgRate).First(),
                WorstMovie = ratedMovies.Where(m => m.AvgRate.HasValue).Last(),
                AvgRate = Math.Round(ratedMovies.Where(m => m.AvgRate.HasValue).Select(m => m.AvgRate).Average().Value, 2),
                Activities = GenerateActivities(ratedMovies)
            };
        }

        private static List<ActivityDto> GenerateActivities(List<MovieRatedDto> movies)
        {
            var result = new List<ActivityDto>();

            movies
            .Where(m => m.SeenDate >= DateTime.Now.AddMonths(-1) || m.AddedDate >= DateTime.Now.AddMonths(-2))
            .ToList()
            .ForEach(m =>
            {
                if (m.SeenDate.HasValue)
                {
                    result.Add(new ActivityDto
                    {
                        Date = m.SeenDate.Value,
                        Title = "A movie has been rated",
                        Description = $"The movie \"{m.Title}\" has been given a {m.AvgRate}/10"
                    });
                }
                else
                {
                    result.Add(new ActivityDto
                    {
                        Date = m.AddedDate,
                        Title = "A movie has been add",
                        Description = $"The movie \"{m.Title}\" is now in the watchlist"
                    }); ;
                }
            });

            return result;
        }

        /// <summary>
        /// Map a movie entitie to a detailled movie DTO
        /// </summary>
        /// <param name="movie">Movie entitie to map</param>
        /// <returns>mapped movie detailled DTO</returns>
        public static MovieRatedDto MapToDTO(this Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            List<RatesMovieDto> ratesMovie = movie.Rates.MapToDTO();

            return new MovieRatedDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                AddedDate = movie.AddedDate,
                SeenDate = movie.SeenDate,
                Rates = ratesMovie,
                Poster = movie.Poster,
                AvgRate = ratesMovie.AvgRate(),
                MaxRate = ratesMovie.MaxRate(),
                MinRate = ratesMovie.MinRate()
            };
        }

        /// <summary>
        /// Map list of movie entities to list of movie detailled DTO
        /// </summary>
        /// <param name="movies">list of movie entities to map</param>
        /// <returns>list of mapped movie detailled DTOs</returns>
        public static List<MovieRatedDto> MapToDTO(this List<Movie> movies)
        {
            if (movies == null) throw new ArgumentNullException(nameof(movies));

            var result = new List<MovieRatedDto>();
            movies.ForEach(movie => result.Add(movie.MapToDTO()));

            return result;
        }

        #endregion MovieDTO mapper

        #region RateDTO mapper

        /// <summary>
        /// Map list of Rate entitie to a list of rate DTO
        /// </summary>
        /// <param name="rates">List of rate entities to map</param>
        /// <returns>List of mapped movie DTO</returns>
        public static List<RatesMovieDto>? MapToDTO(this List<Rate>? rates)
        {
            var result = new List<RatesMovieDto>();

            if (rates is not null)
            {
                rates.ForEach(rate =>
                {
                    result.Add(rate.MapToDTO());
                });
            }

            return result;
        }

        /// <summary>
        /// Map a Rate entitie to a rate DTO
        /// </summary>
        /// <param name="rate">rate entitie to map</param>
        /// <returns>mapped rate DTO</returns>
        public static RatesMovieDto? MapToDTO(this Rate? rate)
        {
            if (rate == null) return null;

            return new RatesMovieDto
            {
                User = rate.User.MapToDTO(),
                Rate = rate.Note,
                Movie = new MovieDto
                {
                    MovieId = rate.MovieId,
                    Title = rate.Movie.Title,
                    Poster = rate.Movie.Poster,
                    AddedDate = rate.Movie.AddedDate,
                    SeenDate = rate.Movie.SeenDate
                }
            };
        }

        #endregion RateDTO mapper

        #region UserDTO mapper

        /// <summary>
        /// Map a User entitie to a user DTO
        /// </summary>
        /// <param name="user">user entitie to map</param>
        /// <returns>mapped user DTO</returns>
        public static UserDto? MapToDTO(this User user)
        {
            return new UserDto { UserId = user.UserId, UserName = user.Name, UserRole = user.Role };
        }

        #endregion UserDTO mapper
    }
}