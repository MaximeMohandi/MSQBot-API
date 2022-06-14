using MSQBot_API.Core.Interfaces.Meters;

namespace MSQBot_API.Core.DTOs.Meters
{
    public record MeterDto : IMeter
    {
        public int MeterId { get; init; }
        public string Name { get; set; }
        public string? Rules { get; set; }

        public List<IScore> Scores { get; set; }
    }
}
