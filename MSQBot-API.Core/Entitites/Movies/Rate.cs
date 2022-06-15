using MSQBot_API.Core.Entitites.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entitites.Movies
{
    [Table("rates")]
    public class Rate
    {
        [Column("rate")]
        public decimal Note { get; set; }

        [Column("fk_user")]
        public long UserId { get; set; }

        [Column("fk_movie")]
        public int MovieId { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}