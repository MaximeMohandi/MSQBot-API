using MSQBot_API.Interfaces;

namespace MSQBot_API.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MSQBotDbContext _context;
        private IMovieRepository _movieRepo;
        private IUserRepository _userRepo;
        private IRateRepository _rateRepo;

        public IMovieRepository Movie
        {
            get
            {
                if (_movieRepo == null)
                {
                    _movieRepo = new MovieRepository(_context);
                }
                return _movieRepo;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_context);
                }
                return _userRepo;
            }
        }

        public IRateRepository Rate
        {
            get
            {
                if (_rateRepo == null)
                {
                    _rateRepo = new RateRepository(_context);
                }
                return _rateRepo;
            }
        }

        public RepositoryWrapper(MSQBotDbContext repositoryContext) => _context = repositoryContext;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}