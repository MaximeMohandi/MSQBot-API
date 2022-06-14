using MSQBot_API.Core.Interfaces.Users;
using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs.Meters
{
    public record MeterCreationDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string Name { get; set; } = string.Empty;
        public List<IUser>? Users { get; set; }
    }
}
