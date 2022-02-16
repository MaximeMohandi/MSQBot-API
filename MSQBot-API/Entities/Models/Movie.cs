using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Entities.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("id_movie")]
        public int MovieId { get; set; }

        [Column("name_movie")]
        public string Title { get; set; } = string.Empty;

        [Column("movie_poster")]
        public string Poster { get; set; } = string.Empty;

        [Column("date_added_movie")]
        public DateTime AddedDate { get; set; }

        [Column("seen_date_movie")]
        public DateTime? SeenDate { get; set; }

        public List<Rate>? Rates { get; set; }
    }
}