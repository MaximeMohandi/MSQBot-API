namespace MSQBot_API.Core.DTOs
{
    public record ExistingUserLoginDto : UserLoginDto
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenValidity { get; set; }
    }
}
