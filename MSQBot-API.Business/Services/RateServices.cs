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
    public class RateServices : IRateServices
    {
        private readonly IRateRepository _repository;

        public RateServices(IRateRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RatesMovieDto>> GetRatesUser(long userId)
        {
            var ratesUser = await _repository.GetRatesUser(userId);
            return ratesUser.MapToDTO();
        }

        public async Task RateMovie(MovieRateCreationDto movieRated, IMovieServices movieServices)
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