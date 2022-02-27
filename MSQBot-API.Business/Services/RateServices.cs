using Microsoft.EntityFrameworkCore;
using MSQBot_API.Core.DTOs;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Interfaces;

namespace MSQBot_API.Business.Services
{
    /// <summary>
    /// Service to manage rates datas
    /// </summary>
    public class RateServices
    {
        private readonly IRateRepository _repository;

        public RateServices(IRateRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all the rate a user had given to a movie.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of rates with movie associated for a user</returns>
        public async Task<List<RatesMovieDto>> GetRatesUser(long userId)
        {
            var ratesUser = await _repository.GetRatesUser(userId);
            return ratesUser.MapToDTO();
        }

        /// <summary>
        /// Add a new rate to a movie in database
        /// </summary>
        /// <param name="movieRated">data used to rate a movie</param>
        public async Task RateMovie(IMovieServices movieServices, MovieRateCreationDto movieRated)
        {
            await _repository.Add(new Rate
            {
                MovieId = movieRated.MoviId,
                UserId = movieRated.UserId,
                Note = movieRated.Rate
            });
            await movieServices.UpdateSeenDate(movieRated.MoviId);
        }

    }
}