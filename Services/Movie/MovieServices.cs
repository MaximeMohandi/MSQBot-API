using MSQBot_API.Entities.DTOs;
using MSQBot_API.Entities.Models;
using MSQBot_API.Extensions;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Services.MovieServices
{
    public class MovieServices
    {
        private IRepositoryWrapper _repoWrapper;
        private IImageScrapperService _imageScrapper;

        private readonly string POSTER_SEARCH = " movie poster";

        public MovieServices(IRepositoryWrapper repoWrapper, IImageScrapperService imageScrapper)
        {
            _repoWrapper = repoWrapper;
            _imageScrapper = imageScrapper;
        }

        /// <summary>
        /// Get all movies with the data attached
        /// </summary>
        /// <returns></returns>
        public MoviesDetailData? GetMoviesData()
        {
            List<MovieDto> movies = GetMovies();

            if (movies.Count > 0)
            {
                return new MoviesDetailData
                {
                    SeenMovieCount = movies.Count(x => x.SeenDate is not null),
                    ToSeeMovieCount = movies.Count(x => x.SeenDate is null),
                    BestMovie = movies.BestMovie(),
                    WorstMovie = movies.WorstMovie(),
                    AvgRate = movies.MoviesAvgRate(),
                    MovieList = movies,
                    Activities = SetMovieActivities(movies)
                };
            }
            return null;
        }

        /// <summary>
        /// Fetch all movies
        /// </summary>
        /// <returns></returns>
        public List<MovieDto> GetMovies()
        {
            var movies = _repoWrapper.Movie.GetAllMovies().ToList();
            List<MovieDto> result = new List<MovieDto>();

            foreach (Movie movie in movies)
            {
                result.Add(new MovieDto
                {
                    _id = movie.MovieId,
                    Title = movie.Name,
                    AddedDate = movie.Added,
                    SeenDate = movie.Seen,
                    TopRate = movie.Rates.MaxRate(),
                    BottomRate = movie.Rates.MinRate(),
                    AvgRate = movie.Rates.AvgRate(),
                    Poster = movie.Poster
                });
            }

            return result;
        }

        /// <summary>
        /// Fetch details on a movie
        /// </summary>
        /// <param name="movieId">id movie to fetch</param>
        /// <returns>A movie with all it's rates</returns>
        public MovieDetailsDto GetMovieDetails(int movieId)
        {
            Movie movie = _repoWrapper.Movie.GetMovie(movieId);
            ;

            return new MovieDetailsDto
            {
                _id = movie.MovieId,
                Title = movie.Name,
                AddedDate = movie.Added,
                SeenDate = movie.Seen,
                TopRate = movie.Rates.MaxRate(),
                BottomRate = movie.Rates.MinRate(),
                AvgRate = movie.Rates.AvgRate(),
                Rates = SetMovieRates(movie),
                Poster = movie.Poster
            };
        }

        /// <summary>
        /// Add a new movie in database
        /// </summary>
        /// <param name="movie">the new movie to insert</param>
        public void AddMovie(MovieInsertDto movie)
        {
            _repoWrapper.Movie.AddMovie(new Movie
            {
                Added = movie.AddedDate,
                Name = movie.Title,
                Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH)
            });
            _repoWrapper.Save();
        }

        /// <summary>
        /// Add a poster to all movie that doesn't have one yet
        /// </summary>
        public void UpdateAllMoviePoster()
        {
            IEnumerable<Movie> movies = _repoWrapper.Movie.GetAllMovies();
            foreach (Movie movie in movies)
            {
                if (movie.Poster is null)
                {
                    movie.Poster = _imageScrapper.FindImage(movie.Name + POSTER_SEARCH);
                    _repoWrapper.Movie.Update(movie);
                }
            }
            _repoWrapper.Save();
        }

        #region utils

        /// <summary>
        ///  Get the rates of a movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        private List<RateDto> SetMovieRates(Movie movie)
        {
            List<RateDto> rates = new List<RateDto>();
            movie.Rates.ToList().ForEach(r =>
            {
                rates.Add(new RateDto
                {
                    User = new UserDto { _id = r.User.UserId, Name = r.User.Name },
                    Rate = r.Note
                });
            });
            return rates;
        }

        /// <summary>
        /// Fetch the last activities of movies
        /// </summary>
        /// <param name="movies"></param>
        /// <returns></returns>
        private List<ActivityDto> SetMovieActivities(List<MovieDto> movies)
        {
            var movieActivity = movies.Where(m => m.SeenDate >= DateTime.Now.AddMonths(-1) || m.AddedDate >= DateTime.Now.AddMonths(-2));
            List<ActivityDto> result = new List<ActivityDto>();

            foreach (MovieDto movie in movieActivity)
            {
                if (movie.SeenDate.HasValue)
                {
                    result.Add(new ActivityDto
                    {
                        Date = movie.SeenDate.Value,
                        Title = "A movie has been rated",
                        Desc = $"The movie \"{movie.Title}\" has been given a {movie.AvgRate}/10"
                    });
                }
                else
                {
                    result.Add(new ActivityDto
                    {
                        Date = movie.AddedDate,
                        Title = "A movie has been add",
                        Desc = $"The movie \"{movie.Title}\" is now in the watchlist"
                    }); ;
                }
            }
            return result;
        }

        #endregion utils
    }
}