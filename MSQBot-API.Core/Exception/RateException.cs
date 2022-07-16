namespace MSQBot_API.Core.Exception
{
    public class RateException : System.Exception
    {
        public RateException(string message) : base(message) { }
    }

    public class NoRatesFound : RateException
    {
        public NoRatesFound() : base("No rates found") { }
    }
}
