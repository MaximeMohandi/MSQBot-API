using Microsoft.IdentityModel.Tokens;
using MSQBot_API.Core.Entities;
using MSQBot_API.Entities.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MSQBot_API.Core.Helpers
{
    /// <summary>
    /// Methods used to manage JWT tokens
    /// </summary>
    public static class JwtHelpers
    {
        private static readonly DateTime _expirationTime = DateTime.Now.AddMinutes(60);

        public static IEnumerable<Claim> GetClaims(this UserTokenDto userToken, Guid tokenId)
        {
            return new Claim[]
            {
                new Claim("UserId", userToken.UserId.ToString()),
                new Claim(ClaimTypes.Name, userToken.UserName),
                new Claim(ClaimTypes.NameIdentifier, tokenId.ToString()),
                new Claim(ClaimTypes.Expiration, _expirationTime.ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
        }

        public static IEnumerable<Claim> GetClaims(this UserTokenDto userAccounts, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userAccounts, Id);
        }

        /// <summary>
        /// Build token for user
        /// </summary>
        /// <param name="model">user to build model</param>
        /// <param name="jwtSettings">token build setting</param>
        /// <returns>token user</returns>
        /// <exception cref="ArgumentException">one of the argument is null</exception>
        public static UserTokenDto GetTokenKey(UserTokenDto model, JwtConfiguration jwtConfiguration)
        {
            try
            {
                if (model == null) throw new ArgumentException(nameof(model));

                Guid TokenId = Guid.Empty;
                var UserToken = new UserTokenDto();
                var claims = GetClaims(model, out TokenId); // used to create return list from user token

                return new UserTokenDto
                {
                    Token = GenerateToken(claims, jwtConfiguration),
                    UserId = model.UserId,
                    UserName = model.UserName,
                    Id = TokenId,
                    ExpiredTime = new DateTimeOffset(_expirationTime).DateTime,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpiredTime = new DateTimeOffset(_expirationTime).DateTime,

                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private static string GenerateToken(IEnumerable<Claim> claims, JwtConfiguration jwtConfiguration)
        {
            byte[] key = Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey);

            JwtSecurityToken JwtToken = new JwtSecurityToken(
                    issuer: jwtConfiguration.ValidIssuer,
                    audience: jwtConfiguration.ValidAudience,
                    claims,
                    notBefore: new DateTimeOffset(DateTime.Now.AddDays(-1)).DateTime, // reject token created yesterday
                    expires: new DateTimeOffset(_expirationTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(JwtToken);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}