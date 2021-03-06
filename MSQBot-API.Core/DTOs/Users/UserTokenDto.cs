namespace MSQBot_API.Core.DTOs.Users
{
    /// <summary>
    /// Token given to authenticated user
    /// </summary>
    public record UserTokenDto
    {
        /// <summary>
        /// Token Id
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Token issued
        /// </summary>
        public string Token { get; init; } = string.Empty;

        /// <summary>
        /// Logged user id
        /// </summary>
        /// <remarks>user id to string to avoid json rounding</remarks>
        public string UserId { get; init; }

        /// <summary>
        /// logged user name
        /// </summary>
        public string UserName { get; init; } = string.Empty;

        /// <summary>
        /// Token used to refresh auth
        /// </summary>
        public string RefreshToken { get; init; } = string.Empty;

        /// <summary>
        /// Token expiration date
        /// </summary>
        public DateTime ExpiredTime { get; init; }

        /// <summary>
        /// Refresh token expiration date
        /// </summary>
        public DateTime RefreshTokenExpirationDate { get; init; }
    }
}