using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Business.DTOs
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
        public string Name { get; set; }
    }
}