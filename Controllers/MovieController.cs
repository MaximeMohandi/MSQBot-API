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
        private readonly IRepositoryWrapper _databaseRepoWrapper;
        private readonly IImageScrapperService _imageScrapper;
        private readonly MovieServices _movieServices;

        public MovieController(ILogger<MovieController> logger, IRepositoryWrapper databaseRepository, IImageScrapperService imageScrapper)
        {
            _logger = logger;
            _databaseRepoWrapper = databaseRepository;
            _imageScrapper = imageScrapper;
            _movieServices = new MovieServices(_databaseRepoWrapper, imageScrapper);
        }

        #region Getter

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_movieServices.GetMoviesData());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server error : " + ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "MovieById")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                return Ok(_movieServices.GetMovieDetails(id));
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
        public IActionResult AddMovie([FromBody] MovieInsertDto movie)
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
                return NoContent();
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