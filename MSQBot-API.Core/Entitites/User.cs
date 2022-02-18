using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id_user")]
        public long UserId { get; set; }

        [Column("name_user")]
        public string Name { get; set; }

        public List<Rate> rates { get; set; }
    }
}