using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Services;

namespace MSQBot_API.Controllers
{
    [Route("api/rates")]
    [ApiController]
    public class RateController : ControllerBase
    {
        /*Dependencies*/
        private readonly ILogger _logger;
        private readonly RateServices _rateServices;

        public RateController(ILogger<RateController> logger, RateServices rateServices)
        {
            _logger = logger;
            _rateServices = rateServices;
        }

        [HttpGet("{userId:long}", Name = "user")]
        public IActionResult Get(long userId)
        {
            try
            {
                var rates = _rateServices.GetRatesUser(userId);

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