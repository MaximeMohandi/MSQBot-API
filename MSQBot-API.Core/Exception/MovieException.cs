namespace MSQBot_API.Core.Exception
{
    public class MovieException : System.Exception
    {
        public MovieException(string message) : base(message)
        {

        }
    }

    public class MovieAlreadyExistException : MovieException
    {
        public MovieAlreadyExistException(string movie) : base($"The movie : {movie} already exist in Database")
        {

        }
    }

    public class NoMovieFoundException : MovieException
    {
        public NoMovieFoundException() : base("No movie found")
        {

        }
    }
}
