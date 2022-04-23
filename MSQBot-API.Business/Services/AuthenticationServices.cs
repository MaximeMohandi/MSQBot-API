using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Exception;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Interfaces;
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

        public async Task<UserTokenDto> Authenticate(UserLoginDto userToAuthenticate)
        {
            if (userToAuthenticate == null) throw new ArgumentNullException(nameof(userToAuthenticate));

            if (IsUserExist(userToAuthenticate))
            {
                User user = await GetUserLogin(userToAuthenticate);
                return await GenerateNewToken(user);

            }
            else
            {
                throw new UserNotFoundException(userToAuthenticate.UserName);
            }
        }

        public async Task<UserTokenDto> RefreshToken(UserRefreshTokenDto authenticatedUser)
        {
            if (authenticatedUser == null) throw new ArgumentNullException(nameof(authenticatedUser));

            if (JwtHelpers.IsValidExpiredAccessToken(authenticatedUser.AccessToken, _jwtSettings)
                && IsUserExist(authenticatedUser))
            {
                User user = await GetUserLogin(authenticatedUser);
                var isValid = user.RefreshTokenValidity > DateTime.Now && user.RefreshToken == authenticatedUser.RefreshToken;

                if (isValid)
                {
                    return await GenerateNewToken(user);
                }
                else
                {
                    throw new Exception("Token is not valid ");
                }
            }
            throw new UserNotFoundException($"user {authenticatedUser.UserName} not found");
        }


        private async Task<UserTokenDto> GenerateNewToken(User existingUser)
        {

            var userToken = JwtHelpers.GetTokenKey(new UserTokenDto
            {
                Id = Guid.NewGuid(),
                UserId = existingUser.UserId.ToString(),
                UserName = existingUser.Name,
            }, _jwtSettings);

            existingUser.RefreshToken = userToken.RefreshToken;
            existingUser.RefreshTokenValidity = userToken.RefreshTokenExpirationDate;
            await _userRepository.Update(existingUser);

            return userToken;
        }

        /// <summary>
        /// Check that user exist in database
        /// </summary>
        /// <param name="user">User to check</param>
        /// <returns>True if user exist, false otherwise.</returns>
        private bool IsUserExist(IUser user)
        {
            return _userRepository.IsUserExist(user.UserId, user.UserName);
        }

        /// <summary>
        /// Get a user from database
        /// </summary>
        /// <param name="user">user asked by the client</param>
        /// <returns>A user entity from database</returns>
        private async Task<User> GetUserLogin(IUser user)
        {
            return await _userRepository.Get(user.UserId);
        }
    }
}