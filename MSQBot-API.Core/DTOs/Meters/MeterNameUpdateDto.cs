using System.ComponentModel.DataAnnotations;

namespace MSQBot_API.Core.DTOs.Meters
{
    public record MeterNameUpdateDto
    {
        [Required(ErrorMessage = "Id meter is required")]
        public int MeterId { get; init; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50")]
        public string NewName { get; set; } = string.Empty;
    }
}
