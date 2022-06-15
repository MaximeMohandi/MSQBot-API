using MSQBot_API.Core.Interfaces.Meters;
using MSQBot_API.Core.Interfaces.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSQBot_API.Core.Entitites.Meters
{
    [Table("score")]
    public class MeterPlayer
    {
        [Column("fk_meter")]
        public int MeterId { get; set; }

        [Column("fk_user")]
        public long UserId { get; set; }

        [Column("score")]
        public int Score { get; set; }

        public IUser User { get; set; }
        public IMeter Meter { get; set; }
    }
}
