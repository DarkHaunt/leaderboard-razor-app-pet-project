using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface IPlayerRepository
{
   Task<List<PlayerEntity>> GetAllPlayersAsync();
   Task<PlayerEntity?> GetPlayerByIdAsync(Guid id);
   Task<PlayerEntity?> GetPlayerTrackedByIdAsync(Guid id);
   Task AddPlayerAsync(Guid id, string nickname, int rating);
   Task UpdatePlayerAsync(Guid id, string nickname, int rating);
   Task DeletePlayerAsync(Guid id);
   Task UpdateLeagueForPlayer(Guid playerId, LeagueEntity? league);
}