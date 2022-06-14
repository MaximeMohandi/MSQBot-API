using MSQBot_API.Core.Enums;
using MSQBot_API.Core.Interfaces.Users;
using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs.Users
{
    public record UserDto : IUser
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public long UserId { get; init; }

        /// <summary>
        /// User Name
        /// </summary>
        [Required]
        public string UserName { get; init; } = string.Empty;

        /// <summary>
        /// User role
        /// </summary>
        public int UserRole { get; init; } = (int)UserRolesEnum.None;
    }
}