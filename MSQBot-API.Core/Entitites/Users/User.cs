using MSQBot_API.Core.Entitites.Movies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entitites.Users
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public long UserId { get; set; }

        [Column("name_user")]
        public string Name { get; set; }

        [Column("role_user")]
        public int Role { get; set; }

        [Column("user_refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("user_refresh_token_validity")]
        public DateTime? RefreshTokenValidity { get; set; }

        public List<Rate> rates { get; set; }
    }
}