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
        private readonly ILogger _logger;
        private readonly IImageScrapperService _imageScrapper;
        private readonly MovieServices _movieServices;

        public MovieController(ILogger<MovieController> logger, MovieServices movieServices, IImageScrapperService imageScrapper)
        {
            _logger = logger;
            _imageScrapper = imageScrapper;
            _movieServices = movieServices;
        }

        #region Getter

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var movies = _movieServices.GetMovies();

                if (movies is null || movies.Count == 0) return NotFound();

                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server error : " + ex.Message);
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
                return StatusCode(500, "Internal Server error : " + ex.Message);
            }
        }

        [HttpGet("movie/poster/{poster}")]
        public IActionResult GetMoviePoster(string poster)
        {
            return Ok(_imageScrapper.FindImage(poster));
        }

        #endregion Getter

        #region Post

        [HttpPost("movie")]
        public IActionResult AddMovie([FromBody] MovieCreationDto movie)
        {
            try
            {
                if (movie is null)
                {
                    return BadRequest("Movie is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Movie model is invalid");
                }

                _movieServices.AddMovie(movie);

                return StatusCode(201, "movie added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion Post

        #region Put

        [HttpPut("movies/poster")]
        public IActionResult SetMoviesPoster()
        {
            try
            {
                _movieServices.UpdateAllMoviePoster();

                return StatusCode(201, "posters added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion Put
    }
}