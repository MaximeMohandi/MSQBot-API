namespace MSQBot_API.Interfaces
{
    public interface IRepositoryWrapper
    {
        IMovieRepository Movie { get; }
        IUserRepository User { get; }
        IRateRepository Rate { get; }

        void Save();
    }
}