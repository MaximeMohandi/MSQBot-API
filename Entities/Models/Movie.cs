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
        public string? Name { get; set; }

        [Column("movie_poster")]
        public string? Poster { get; set; }

        [Column("date_added_movie")]
        public DateTime Added { get; set; }

        [Column("seen_date_movie")]
        public DateTime? Seen { get; set; }

        public ICollection<Rate>? Rates { get; set; }
    }
}