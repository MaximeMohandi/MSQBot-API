using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Interfaces;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Business.Services
{
    /// <summary>
    /// Services to manage movies data.
    /// </summary>
    public class MovieServices : IMovieServices
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
        /// Get a view with all movies and key data
        /// </summary>
        /// <returns></returns>
        public async Task<MoviesViewDto> GetMoviesView()
        {
            List<Movie> movies = await _repository.GetAll();
            return new MoviesViewDto(movies.MapToMovieRatedDtoDTOs());
        }

        /// <summary>
        /// Fetch details on a movie
        /// </summary>
        /// <param name="movieId">id movie to fetch</param>
        /// <returns>A movie with all it's rates</returns>
        public async Task<MovieRatedDto> GetMovie(int movieId)
        {
            Movie movie = await _repository.Get(movieId);
            return movie.MapToMovieRatedDto();
        }

        /// <summary>
        /// Fetch all movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<MovieDto>> GetMovies()
        {
            return EntityMapper.MapToMovieDTOs(await _repository.GetAll());
        }

        /// <summary>
        /// Add a new movie in database
        /// </summary>
        /// <param name="movie">the new movie to insert</param>
        public async Task AddMovie(MovieCreationDto movie)
        {
            await _repository.Add(new Movie
            {
                Title = movie.Title,
                Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH)
            });
        }

        /// <summary>
        /// Add a poster to all movie that doesn't have one yet
        /// </summary>
        public async Task UpdateAllMoviePoster()
        {
            var movies = await _repository.GetAll();
            foreach (Movie movie in movies.Where(m => m.Poster is null))
            {
                    movie.Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH);
                    await _repository.Update(movie);
            }
        }

        /// <summary>
        /// Update seen date of movie if it's not already set.
        /// </summary>
        /// <param name="movie">movie to update</param>
        public async Task UpdateMovieSeenDate(int movieId)
        {
            var movieToUpdate = await _repository.Get(movieId);

            if (movieToUpdate.SeenDate is null)
            {
                movieToUpdate.SeenDate = DateTime.Now;
                await _repository.Update(movieToUpdate);
            }
        }

        /// <summary>
        /// Replace the title of a movie.
        /// </summary>
        /// <param name="newMovieTitle">DTO representing the change</param>
        public async Task UpdateMovieName(MovieTitleUpdateDto newMovieTitle)
        {
            if (newMovieTitle.NewTitle is null || newMovieTitle.NewTitle == string.Empty)
                throw new ArgumentException("New Title can't be null");

            await _repository.Update(new Movie
            {
                MovieId = newMovieTitle.MovieId,
                Title = newMovieTitle.NewTitle
            });
        }
    }
}