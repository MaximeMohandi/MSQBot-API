using MSQBot_API.Core.DTOs;
using MSQBot_API.Entities.DTOs;

namespace MSQBot_API.Interfaces
{
    /// <summary>
    /// Base method to implemente to authenticate a user from a services.
    /// </summary>
    public interface IUserAuthenticationServices
    {
        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="user">user to authenticate</param>
        /// <returns> Send an authorization token</returns>
        public Task<UserTokenDto> Authenticate(UserLoginDto user);

        /// <summary>
        /// Refresh a user token
        /// </summary>
        /// <returns>new user Token</returns>
        public Task<UserTokenDto> RefreshToken(UserRefreshTokenDto userToken);
    }
}