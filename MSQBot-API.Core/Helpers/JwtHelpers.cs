using Microsoft.IdentityModel.Tokens;
using MSQBot_API.Core.Entities;
using MSQBot_API.Entities.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSQBot_API.Core.Helpers
{
    /// <summary>
    /// Methods used to manage JWT tokens
    /// </summary>
    public static class JwtHelpers
    {
        private static readonly DateTime _expirationTime = DateTime.Now.AddMinutes(1);

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
        public static UserTokenDto GetTokenKey(UserTokenDto model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new UserTokenDto();
                if (model == null) throw new ArgumentException(nameof(model));

                var key = Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey);
                Guid Id = Guid.Empty;

                var JWToken = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id), //used to create return list from user token
                    /*  notBefore: new DateTimeOffset(DateTime.Now.AddDays(-1)).DateTime,*/
                    expires: new DateTimeOffset(_expirationTime).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                return new UserTokenDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(JWToken),
                    UserId = model.UserId,
                    UserName = model.UserName,
                    Id = Id,
                    ExpiredTime = new DateTimeOffset(_expirationTime).DateTime
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}