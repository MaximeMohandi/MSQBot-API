namespace MSQBot_API.Entities.DTOs
{
    public class MovieDetailsDto : MovieDto
    {
        /// <summary>
        /// All rates given to the movie
        /// </summary>
        public ICollection<RateDto>? Rates { get; set; }
    }
}