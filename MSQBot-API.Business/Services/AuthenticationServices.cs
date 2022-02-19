﻿using MSQBot_API.Core.DTOs;
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
        private readonly JwtSettings _jwtSettings;

        public AuthenticationServices(IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public UserTokenDto Authenticate(UserDto userToAuthenticate)
        {
            if (userToAuthenticate == null) throw new ArgumentNullException(nameof(userToAuthenticate));

            if (IsUserExist(userToAuthenticate))
            {
                var user = GetUser(userToAuthenticate);
                return JwtHelpers.GetTokenKey(new UserTokenDto
                {
                    Id = Guid.NewGuid(),
                    UserId = userToAuthenticate.UserId,
                    UserName = userToAuthenticate.Name,
                }, _jwtSettings);
            }

            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Refresh user token
        /// </summary>
        /// <param name="authentifiedUser"></param>
        /// <returns></returns>
        public UserTokenDto RefreshToken(UserTokenDto authentifiedUser)
        {
            throw new NotImplementedException();
        }

        private bool IsUserExist(UserDto user)
        {
            return _userRepository.IsUserExist(new User { Name = user.Name, UserId = user.UserId });
        }

        /// <summary>
        /// Get a user from database
        /// </summary>
        /// <param name="user">user asked by the client</param>
        /// <returns>A user entity from database</returns>
        /// <exception cref="KeyNotFoundException">No user has been found</exception>
        private async Task<User> GetUser(UserDto user)
        {
            return await _userRepository.Get(user.UserId);
        }
    }
}