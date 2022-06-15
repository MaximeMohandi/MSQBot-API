using MSQBot_API.Core.DTOs.Meters;
using MSQBot_API.Core.Interfaces.Meters;
using MSQBot_API.Core.Interfaces.Users;

namespace MSQBot_API.Business.Interfaces.Meters
{
    public interface IMeterServices : IBusinessServices<MeterDto, MeterCreationDto>
    {
        Task UpdateScore(IScore newScore);

        Task UpdateMeterName(MeterNameUpdateDto newMeterTitle);

        Task AddPlayerToMeter(IUser newPlayer);

        Task AddPlayersToMeter(IEnumerable<IUser> newPlayers);

        Task RemovePlayerFromMeter(IUser toRemovePlayer);

    }
}
