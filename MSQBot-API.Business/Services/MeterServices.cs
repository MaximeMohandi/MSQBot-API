using MSQBot_API.Business.Interfaces.Meters;
using MSQBot_API.Core.DTOs.Meters;
using MSQBot_API.Core.Entitites.Meters;
using MSQBot_API.Core.Interfaces.Meters;
using MSQBot_API.Core.Interfaces.Users;
using MSQBot_API.Core.Repositories;

namespace MSQBot_API.Business.Services
{
    public class MeterServices : IMeterServices
    {

        private readonly IMeterRepository _repository;

        public MeterServices(IMeterRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(MeterCreationDto entity)
        {
            await _repository.Add(new Meter
            {
                Name = entity.Name,
                Rules = entity.Rules
            });

            if (entity.Players != null)
            {
                await AddPlayersToMeter(entity.Players);
            }

        }

        public Task AddPlayersToMeter(IEnumerable<IUser> newPlayers)
        {
            throw new NotImplementedException();
        }

        public Task AddPlayerToMeter(IUser newPlayer)
        {
            throw new NotImplementedException();
        }

        public Task<MeterDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MeterDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public Task RemovePlayerFromMeter(IUser toRemovePlayer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMeterName(MeterNameUpdateDto newMeterTitle)
        {
            throw new NotImplementedException();
        }

        public Task UpdateScore(IScore newScore)
        {
            throw new NotImplementedException();
        }
    }
}
