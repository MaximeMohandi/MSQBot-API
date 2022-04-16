namespace MSQBot_API.Core.Interfaces
{
    public interface IUser
    {
        public long UserId { get; init; }

        public string UserName { get; init; }
    }
}
