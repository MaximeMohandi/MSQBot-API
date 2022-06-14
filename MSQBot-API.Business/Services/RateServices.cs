using MSQBot_API.Business.Interfaces.Movies;
using MSQBot_API.Core.DTOs.Movies;
using MSQBot_API.Core.Entitites.Movies;
using MSQBot_API.Core.Helpers;
using MSQBot_API.Core.Repositories;

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
            var userRates = await _repository.GetRatesUser(movieRated.UserId);
            var existingRate = userRates.FirstOrDefault(r => r.MovieId == movieRated.MovieId);


            if (existingRate is not null)
            {
                existingRate.Note = movieRated.Rate;
                await _repository.Update(existingRate);
            }
            else
            {
                await _repository.Add(new Rate
                {
                    MovieId = movieRated.MovieId,
                    UserId = movieRated.UserId,
                    Note = movieRated.Rate
                });
            }

            await movieServices.UpdateSeenDate(movieRated.MovieId);

        }

    }
}