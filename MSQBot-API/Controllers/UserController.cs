using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Exception;
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
        public async Task<ActionResult<UserTokenDto>> AuthenticateUser([FromBody] UserDto user)
        {
            try
            {
                var authToken = await _userAuthenticationServices.Authenticate(user);
                return Ok(authToken);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException)
            {
                return NotFound(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}