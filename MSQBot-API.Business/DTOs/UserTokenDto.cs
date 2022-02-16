namespace MSQBot_API.Entities.DTOs
{
    /// <summary>
    /// Token given to authenticated user
    /// </summary>
    public class UserTokenDto
    {
        /// <summary>
        /// Token Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Token issued
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Logged user id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// logged user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Token validity time
        /// </summary>
        public TimeSpan Validity { get; set; }

        /// <summary>
        /// Token used to refresh auth
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Token expiration date
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}