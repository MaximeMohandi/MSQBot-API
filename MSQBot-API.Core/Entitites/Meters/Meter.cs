using MSQBot_API.Core.Interfaces.Meters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entitites.Meters
{
    [Table("meters")]
    public class Meter : IMeter
    {
        [Key]
        [Column("id_meter")]
        public int MeterId { get; init; }

        [Column("name_meter")]
        public string Name { get; set; }

        [Column("rules_meter")]
        public string? Rules { get; set; }
    }
}
