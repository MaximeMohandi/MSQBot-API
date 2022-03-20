using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}