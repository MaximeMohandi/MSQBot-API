using MSQBot_API.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs
{
    public record UserLoginDto : IUser
    {
        [Required]
        public long UserId { get; init; }

        [Required]
        public string UserName { get; init; } = string.Empty;

    }
}
