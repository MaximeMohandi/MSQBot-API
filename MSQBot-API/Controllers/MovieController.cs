using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Business.Interfaces.Movies;
using MSQBot_API.Core.DTOs.Movies;
using MSQBot_API.Core.Exception;
using MSQBot_API.Messages;

namespace MSQBot_API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovieController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMovieServices _movieServices;
        private readonly IRateServices _rateServices;


        public MovieController(ILogger<MovieController> logger,
            IMovieServices movieServices,
            IRateServices rateServices)
        {
            _logger = logger;
            _movieServices = movieServices;
            _rateServices = rateServices;
        }

        #region Getter

        /// <summary>
        /// Get All the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var movies = await _movieServices.GetGlobalView();

                return Ok(movies);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch (MovieException ex)
            {
                var erroMsg = $"{MovieMessages.ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
                _logger.LogError(erroMsg);
                return StatusCode(500, erroMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}", Name = "Get Movie by Id")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var movie = await _movieServices.Get(id);

                return Ok(movie);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch (MovieException ex)
            {
                var erroMsg = $"{MovieMessages.ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
                _logger.LogError(erroMsg);
                return StatusCode(500, erroMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{title}", Name = "Get Movie by Title")]
        public async Task<IActionResult> GetAsync(string title)
        {
            try
            {
                var movie = await _movieServices.Get(title);

                return Ok(movie);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch (MovieException ex)
            {
                var erroMsg = $"{MovieMessages.ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
                _logger.LogError(erroMsg);
                return StatusCode(500, erroMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpGet("wallpaper/{movie}", Name = "getMovieWallpaper")]
        public async Task<IActionResult> GetWallpaperMovie(string movie)
        {
            try
            {
                var wallpaper = await _movieServices.GetMovieWallpaper(movie);
                return Ok(wallpaper);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("random", Name = "Get Random Movie")]
        public async Task<IActionResult> GetRandom()
        {
            try
            {
                var movie = await _movieServices.GetRandomMovie();

                return Ok(movie);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch (MovieException ex)
            {
                var erroMsg = $"{MovieMessages.ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
                _logger.LogError(erroMsg);
                return StatusCode(500, erroMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        #endregion Getter

        #region Post

        [HttpPost("movie")]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreationDto movie)
        {
            try
            {
                if (movie is null) return BadRequest(MovieMessages.ERR_MOVIE_ARGS_NULL);

                if (!ModelState.IsValid) return BadRequest(MovieMessages.ERR_MOVIE_INVALID_BODY);

                await _movieServices.Add(movie);

                return StatusCode(201, MovieMessages.SUCCESS_MOVIE_ADDED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(302, ex.Message);
            }
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateMovieAsync([FromBody] MovieRateCreationDto movieRated)
        {
            try
            {
                if (movieRated == null) return BadRequest(MovieMessages.ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(MovieMessages.ERR_MOVIE_INVALID_BODY);

                await _rateServices.RateMovie(movieRated, _movieServices);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, MovieMessages.ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        #endregion Post

        #region Put

        [HttpPut("poster")]
        public async Task<IActionResult> SetMoviesPosterAsync()
        {
            try
            {
                await _movieServices.UpdateAllMoviePoster();

                return StatusCode(202, MovieMessages.SUCCESS_MOVIE_UPDATED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, MovieMessages.ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpPut("movie/name")]
        public async Task<IActionResult> UpdateMovieNameAsync([FromBody] MovieTitleUpdateDto newNameMovie)
        {
            try
            {
                if (newNameMovie == null) return BadRequest(MovieMessages.ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(MovieMessages.ERR_MOVIE_INVALID_BODY);

                await _movieServices.UpdateName(newNameMovie);

                return StatusCode(202, MovieMessages.SUCCESS_MOVIE_UPDATED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, MovieMessages.ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        #endregion Put
    }
}