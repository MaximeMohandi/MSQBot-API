namespace MSQBot_API.Core.Interfaces.Users
{
    public interface IUser
    {
        public long UserId { get; init; }

        public string UserName { get; init; }
    }
}
