using Microsoft.EntityFrameworkCore;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Entities.Models;
using MSQBot_API.Helpers;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Services.MovieServices
{
    /// <summary>
    /// Services to manage movies data.
    /// </summary>
    public class MovieServices
    {
        private readonly MSQBotDbContext _dbContext;
        private readonly IImageScrapperService _imageScrapper;

        private readonly string POSTER_SEARCH = " movie poster";
        private readonly string NO_MOVIE_ERR = "No movie found";

        public MovieServices(MSQBotDbContext dbContext, IImageScrapperService imageScrapper)
        {
            _dbContext = dbContext;
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
        public List<MovieDetailsDto> GetMovies()
        {
            return _dbContext.Movies
                .Include(r => r.Rates)
                .ThenInclude(r => r.User)
                .ToList()
                .MapToMovieDetailsDTOs();
        }

        /// <summary>
        /// Add a new movie in database
        /// </summary>
        /// <param name="movie">the new movie to insert</param>
        public void AddMovie(MovieCreationDto movie)
        {
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
    }
}