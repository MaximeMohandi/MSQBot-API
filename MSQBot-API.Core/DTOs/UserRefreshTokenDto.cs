namespace MSQBot_API.Core.DTOs
{
    public record UserRefreshTokenDto : UserLoginDto
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
