using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Interfaces;
using MSQBot_API.Services.MovieServices;

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

        /*Movies message codes*/
        private const string ERR_MOVIE_INTERNAL_SERVER = "ERR_MOVIE_INTERNAL_SERVER";
        private const string ERR_MOVIE_ARGS_NULL = "ERR_MOVIE_ARGS_NULL";
        private const string ERR_MOVIE_INVALID_BODY = "ERR_MOVIE_INVALID_BODY";
        private const string SUCCESS_MOVIE_ADDED = "SUCCESS_MOVIE_ADDED";
        private const string SUCCESS_MOVIE_UPDATED = "SUCCESS_MOVIE_UPDATED";

        public MovieController(ILogger<MovieController> logger, MovieServices movieServices, IImageScrapperService imageScrapper)
        {
            _logger = logger;
            _imageScrapper = imageScrapper;
            _movieServices = movieServices;
        }

        #region Getter

        /// <summary>
        /// Get All the movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
        {
            try
            {
                var movies = _movieServices.GetMoviesData();

                if (movies is null) return NotFound();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpGet("{id:int}", Name = "getMovie")]
        public IActionResult Get(int id)
        {
            try
            {
                var movie = _movieServices.GetMovie(id);

                if (movie is null) return NotFound();

                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpGet("movie/poster/{poster}")]
        public IActionResult GetMoviePoster(string poster)
        {
            if (poster is null || poster == string.Empty) return BadRequest(ERR_MOVIE_ARGS_NULL);
            return Ok(_imageScrapper.FindImage(poster));
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
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpPost("rate")]
        public IActionResult RateMovie([FromBody] MovieRateCreationDto movieRated)
        {
            try
            {
                if (movieRated == null) return BadRequest(ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(ERR_MOVIE_INVALID_BODY);

                _movieServices.RateMovie(movieRated);

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
        public IActionResult SetMoviesPoster()
        {
            try
            {
                _movieServices.UpdateAllMoviePoster();

                return StatusCode(202, SUCCESS_MOVIE_UPDATED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ERR_MOVIE_INTERNAL_SERVER);
            }
        }

        [HttpPut("movie/name")]
        public IActionResult UpdateMovieName([FromBody] MovieTitleUpdateDto newNameMovie)
        {
            try
            {
                if (newNameMovie == null) return BadRequest(ERR_MOVIE_ARGS_NULL);
                if (!ModelState.IsValid) return BadRequest(ERR_MOVIE_INVALID_BODY);

                _movieServices.UpdateMovieName(newNameMovie);

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