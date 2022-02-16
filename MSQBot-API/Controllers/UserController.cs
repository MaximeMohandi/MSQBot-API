using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IUserAuthenticationServices _userAuthenticationServices;

        public UserController(
            IConfiguration configuration,
            ILogger<UserController> logger,
            IUserAuthenticationServices userAuthentication)
        {
            _config = configuration;
            _logger = logger;
            _userAuthenticationServices = userAuthentication;
        }

        [HttpPost("authenticate")]
        public ActionResult<UserTokenDto> AuthenticateUser([FromBody] UserDto user)
        {
            try
            {
                return Ok(_userAuthenticationServices.Authenticate(user));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}