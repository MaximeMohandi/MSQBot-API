using MSQBot_API.Core.Interfaces.Users;

namespace MSQBot_API.Core.Interfaces.Meters
{
    public interface IScore
    {
        IMeter Meter { get; set; }
        IUser User { get; set; }
        public decimal? Score { get; init; }
    }
}
