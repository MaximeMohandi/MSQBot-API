using Microsoft.IdentityModel.Tokens;
using MSQBot_API.Entities.DTOs;
using MSQBot_API.Entities.Models;
using MSQBot_API.Helpers;
using MSQBot_API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.AspNetCore.Http.HttpRequest;

namespace MSQBot_API.Services
{
    public class AuthenticationServices : IUserAuthenticationServices
    {
        private readonly MSQBotDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationServices(MSQBotDbContext dbContext, JwtSettings jwtSettings)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings;
        }

        public UserTokenDto Authenticate(UserDto userToAuthenticate)
        {
            if (userToAuthenticate == null) throw new ArgumentNullException(nameof(userToAuthenticate));

            if (IsUserExist(userToAuthenticate))
            {
                var user = GetUser(userToAuthenticate);
                return JwtHelpers.GetTokenKey(new UserTokenDto()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.UserId,
                    UserName = user.Name,
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
            return _dbContext.Users.Any(u => u.UserId == user.UserId);
        }

        /// <summary>
        /// Get a user from database
        /// </summary>
        /// <param name="user">user asked by the client</param>
        /// <returns>A user entity from database</returns>
        /// <exception cref="KeyNotFoundException">No user has been found</exception>
        private User GetUser(UserDto user)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Name == user.Name)
                ?? throw new KeyNotFoundException();
        }
    }
}