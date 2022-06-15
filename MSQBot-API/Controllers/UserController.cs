using Microsoft.AspNetCore.Mvc;
using MSQBot_API.Business.Interfaces.User;
using MSQBot_API.Core.DTOs.Users;
using MSQBot_API.Core.Exception;

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
        public async Task<ActionResult<UserTokenDto>> AuthenticateUser([FromBody] UserLoginDto user)
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
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<UserTokenDto>> RefreshTokenUser([FromBody] UserRefreshTokenDto refreshTokenUser)
        {
            try
            {
                var authToken = await _userAuthenticationServices.RefreshToken(refreshTokenUser);
                return Ok(authToken);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}