using MSQBot_API.Business.DTOs;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Interfaces;
using MSQBot_API.Business.Mappers;

namespace MSQBot_API.Services.MovieServices
{
    /// <summary>
    /// Services to manage movies data.
    /// </summary>
    public class MovieServices
    {
        private readonly IMovieRepository _repository;
        private readonly IImageScrapperService _imageScrapper;

        private readonly string POSTER_SEARCH = " movie poster";
        private readonly string MOVIE_EXIST_ERR = "The Movie you try to add already exist";

        public MovieServices(IMovieRepository repository, IImageScrapperService imageScrapper)
        {
            _repository = repository;
            _imageScrapper = imageScrapper;
        }

        /// <summary>
        /// Get all movies with the data attached
        /// </summary>
        /// <returns></returns>
        public MovieDatasDto GetMoviesData()
        {
            return new MovieDatasDto(GetMovies());
        }

        /// <summary>
        /// Fetch details on a movie
        /// </summary>
        /// <param name="movieId">id movie to fetch</param>
        /// <returns>A movie with all it's rates</returns>
        public MovieDetailsDto GetMovie(int movieId)
        {
            return GetMovies().FirstOrDefault(m => m.MovieId == movieId);
        }

        /// <summary>
        /// Fetch all movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieDto>> GetMovies()
        {
            return  MovieMapper.MapToMovieDTOs(await _repository.GetAll());
        }

        /// <summary>
        /// Add a new movie in database
        /// </summary>
        /// <param name="movie">the new movie to insert</param>
        public void AddMovie(MovieCreationDto movie)
        {
            if (_dbContext.Movies.Any(m => m.Title == movie.Title)) throw new ArgumentException(MOVIE_EXIST_ERR);

            _dbContext.Movies.Add(new Movie
            {
                Title = movie.Title,
                Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH)
            });
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add a poster to all movie that doesn't have one yet
        /// </summary>
        public void UpdateAllMoviePoster()
        {
            foreach (Movie movie in _dbContext.Movies)
            {
                if (movie.Poster is null)
                {
                    movie.Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH);
                    _dbContext.Movies.Update(movie);
                }
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add a new rate to a movie in database
        /// </summary>
        /// <param name="movieRated">data used to rate a movie</param>
        public void RateMovie(MovieRateCreationDto movieRated)
        {
            _dbContext.Rates.Add(new Rate
            {
                MovieId = movieRated.MoviId,
                UserId = movieRated.UserId,
                Note = movieRated.Rate
            });
            UpdateMovieSeenDate(_dbContext.Movies.FirstOrDefault(m => m.MovieId == movieRated.MoviId));
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Update seen date of movie if it's not already set.
        /// </summary>
        /// <param name="movie">movie to update</param>
        public void UpdateMovieSeenDate(Movie movie)
        {
            if (movie.SeenDate is null)
            {
                movie.SeenDate = DateTime.Now;
                _dbContext.Movies.Update(movie);
                _dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Replace the title of a movie.
        /// </summary>
        /// <param name="newMovieTitle">DTO representing the change</param>
        public void UpdateMovieName(MovieTitleUpdateDto newMovieTitle)
        {
            if (newMovieTitle.NewTitle is null)
                throw new ArgumentException("New Title can't be null");

            _dbContext.Movies.Update(new Movie
            {
                MovieId = newMovieTitle.MovieId,
                Title = newMovieTitle.NewTitle
            });
            _dbContext.SaveChanges();
        }
    }
}