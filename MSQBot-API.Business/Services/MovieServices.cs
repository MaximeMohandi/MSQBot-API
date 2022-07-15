using MSQBot_API.Business.Interfaces.Movies;
using MSQBot_API.Core.DTOs.Movies;
using MSQBot_API.Core.Entitites.Movies;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Interfaces.Movies;
using MSQBot_API.Core.Repositories;

namespace MSQBot_API.Business.Services
{
    /// <summary>
    /// Services to manage movies data.
    /// </summary>
    public class MovieServices : IMovieServices, IContainPoster
    {
        private readonly IMovieRepository _repository;
        private readonly IImageScrapperService _imageScrapper;

        private readonly string POSTER_SEARCH = " movie poster 2160p";
        private readonly string WALLPAPER_SEARCH = " movie wallpaper 2160p";

        public MovieServices(IMovieRepository repository, IImageScrapperService imageScrapper)
        {
            _repository = repository;
            _imageScrapper = imageScrapper;
        }

        public async Task<MoviesViewDto> GetGlobalView()
        {
            List<Movie> movies = await _repository.GetAll();
            return movies.MapToViewDto();
        }

        public async Task<MovieRatedDto> Get(int id)
        {
            Movie movie = await _repository.Get(id);
            return movie.MapToDTO();
        }

        public async Task<MovieRatedDto> Get(string title)
        {
            Movie movie = await _repository.Get(title);
            return movie.MapToDTO();
        }

        public async Task<List<MovieRatedDto>> GetAll()
        {
            var movies = await _repository.GetAll();
            return movies.MapToDTO();
        }

        public async Task Add(MovieCreationDto movie)
        {
            await _repository.Add(new Movie
            {
                Title = movie.Title,
                AddedDate = DateTime.Now,
                Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH)
            });
        }

        public async Task UpdateAllMoviePoster()
        {
            var movies = await _repository.GetAll();
            foreach (Movie movie in movies.Where(m => m.Poster is null))
            {
                movie.Poster = _imageScrapper.FindImage(movie.Title + POSTER_SEARCH);
                await _repository.Update(movie);
            }
        }

        public async Task UpdateSeenDate(int movieId)
        {
            var movieToUpdate = await _repository.Get(movieId);

            if (movieToUpdate.SeenDate is null)
            {
                movieToUpdate.SeenDate = DateTime.Now;
                await _repository.Update(movieToUpdate);
            }
        }

        public async Task UpdateName(MovieTitleUpdateDto newMovieTitle)
        {
            if (newMovieTitle.NewTitle == string.Empty)
                throw new ArgumentException("New Title can't be null");

            await _repository.Update(new Movie
            {
                MovieId = newMovieTitle.MovieId,
                Title = newMovieTitle.NewTitle
            });
        }

        public async Task<string> GetMovieWallpaper(string title)
        {
            return _imageScrapper.FindImage(title + WALLPAPER_SEARCH);
        }

        public async Task<IMovie> GetRandomMovie()
        {
            var movies = await GetAll();
            var toWatchMovies = movies.Where(m => m.SeenDate == null);

            Random random = new Random();
            int randomItem = random.Next(0, toWatchMovies.Count());

            return toWatchMovies.Skip(randomItem).Take(1).First(); //skip a random number of elements (max nb movie) and select 1

        }
    }
}