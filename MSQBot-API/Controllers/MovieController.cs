using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Business.Services;
using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Exception;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        /*Dependencies*/
        private readonly ILogger _logger;
        private readonly IImageScrapperService _imageScrapper;
        private readonly MovieServices _movieServices;
        private readonly RateServices _rateServices;

        /*Movies message codes*/
        private const string ERR_MOVIE_INTERNAL_SERVER = "ERR_MOVIE_INTERNAL_SERVER";
        private const string ERR_MOVIE_ARGS_NULL = "ERR_MOVIE_ARGS_NULL";
        private const string ERR_MOVIE_INVALID_BODY = "ERR_MOVIE_INVALID_BODY";
        private const string SUCCESS_MOVIE_ADDED = "SUCCESS_MOVIE_ADDED";
        private const string SUCCESS_MOVIE_UPDATED = "SUCCESS_MOVIE_UPDATED";

        public MovieController(ILogger<MovieController> logger, 
            MovieServices movieServices, 
            RateServices rateServices,
            IImageScrapperService imageScrapper)
        {
            _logger = logger;
            _imageScrapper = imageScrapper;
            _movieServices = movieServices;
            _rateServices = rateServices;
        }

        #region Getter

        /// <summary>
        /// Get All the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var movies = await _movieServices.GetMoviesView();

                return Ok(movies);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch(MovieException ex)
            {
                var erroMsg = $"{ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
                _logger.LogError(erroMsg);
                return StatusCode(500, erroMsg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var movie = await _movieServices.GetMovie(id);

                return Ok(movie);
            }
            catch (NoMovieFoundException)
            {
                return NotFound();
            }
            catch (MovieException ex)
            {
                var erroMsg = $"{ERR_MOVIE_INTERNAL_SERVER}: {ex.Message}";
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
        public IActionResult AddMovie([FromBody] MovieCreationDto movie)
        {
            try
            {
                if (movie is null) return BadRequest(ERR_MOVIE_ARGS_NULL);

                if (!ModelState.IsValid) return BadRequest(ERR_MOVIE_INVALID_BODY);

                _movieServices.AddMovie(movie);

                return StatusCode(201, SUCCESS_MOVIE_ADDED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("rate")]
        public async Task<IActionResult> RateMovieAsync([FromBody] MovieRateCreationDto movieRated)
        {
            try
            {
                if (movieRated == null) return BadRequest(ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(ERR_MOVIE_INVALID_BODY);

                await _rateServices.RateMovie(_movieServices, movieRated);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
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

                return StatusCode(202, SUCCESS_MOVIE_UPDATED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpPut("movie/name")]
        public async Task<IActionResult> UpdateMovieNameAsync([FromBody] MovieTitleUpdateDto newNameMovie)
        {
            try
            {
                if (newNameMovie == null) return BadRequest(ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(ERR_MOVIE_INVALID_BODY);

                await _movieServices.UpdateMovieName(newNameMovie);

                return StatusCode(202, SUCCESS_MOVIE_UPDATED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        #endregion Put
    }
}