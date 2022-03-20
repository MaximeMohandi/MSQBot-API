using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Business.Services
{
    public class AuthenticationServices : IUserAuthenticationServices
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtConfiguration _jwtSettings;

        public AuthenticationServices(IUserRepository userRepository, JwtConfiguration jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<UserTokenDto> Authenticate(UserDto userToAuthenticate)
        {
            if (userToAuthenticate == null) throw new ArgumentNullException(nameof(userToAuthenticate));

            if (IsUserExist(userToAuthenticate))
            {
                User user = await GetUser(userToAuthenticate);
                return JwtHelpers.GetTokenKey(new UserTokenDto
                {
                    Id = Guid.NewGuid(),
                    UserId = user.UserId,
                    UserName = user.Name,
                }, _jwtSettings);
            }

            throw new KeyNotFoundException();
        }

        public Task<UserTokenDto> RefreshToken(UserTokenDto authentifiedUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check that user exist in database
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>True if user exist, false otherwise.</returns>
        private bool IsUserExist(UserDto user)
        {
            return _userRepository.IsUserExist(new User { Name = user.UserName, UserId = user.UserId });
        }

        /// <summary>
        /// Get a user from database
        /// </summary>
        /// <param name="user">user asked by the client</param>
        /// <returns>A user entity from database</returns>
        private async Task<User> GetUser(UserDto user)
        {
            return await _userRepository.Get(user.UserId);
        }
    }
}