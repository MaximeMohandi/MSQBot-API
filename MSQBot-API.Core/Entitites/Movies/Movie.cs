using MSQBot_API.Core.Interfaces.Movies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entitites.Movies
{
    [Table("movies")]
    public class Movie : IMovie
    {
        [Key]
        [Column("id_movie")]
        public int MovieId { get; init; }

        [Column("name_movie")]
        public string Title { get; set; } = string.Empty;

        [Column("movie_poster")]
        public string? Poster { get; set; } = string.Empty;

        [Column("date_added_movie")]
        public DateTime AddedDate { get; set; }

        [Column("seen_date_movie")]
        public DateTime? SeenDate { get; set; }

        public List<Rate>? Rates { get; set; }
    }
}