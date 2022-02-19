namespace MSQBot_API.Core.Exception
{
    public class UserException : System.Exception
    {
        public UserException(string message) : base(message) { }
    }

    public class UserNotFoundException : UserException
    {
        public UserNotFoundException(string userName) : base($"No entry foud for user '{userName}'.") { }
        public UserNotFoundException(int id) : base($"No entry foud for user id '{id}'.") { }
    }

}
