using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Business.Interfaces.Movies;

namespace MSQBot_API.Controllers
{
    [Route("api/rates")]
    [ApiController]
    public class RateController : ControllerBase
    {
        /*Dependencies*/
        private readonly ILogger _logger;
        private readonly IRateServices _rateServices;

        public RateController(ILogger<RateController> logger, IRateServices rateServices)
        {
            _logger = logger;
            _rateServices = rateServices;
        }

        [HttpGet("{userId:long}", Name = "user")]
        public async Task<IActionResult> Get(long userId)
        {
            try
            {
                var rates = await _rateServices.GetRatesUser(userId);

                if (rates == null) return NotFound();

                return Ok(rates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error : Something went wrong when fetching datas for user " + userId);
            }
        }
    }
}