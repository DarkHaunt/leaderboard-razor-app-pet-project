using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface ILeagueRepository
{
   Task<List<LeagueEntity>> GetAllLLeaguesAsync();
   Task<LeagueEntity?> GetLeagueByIdAsync(Guid id);
   Task<LeagueEntity?> GetLeagueByIdTrackedAsync(Guid id);
   Task AddLeagueAsync(Guid id, string name, string description, int requiredRating);
   Task UpdateLeagueAsync(Guid id, string name, string description, int requiredRating);
   Task DeleteLeagueAsync(Guid id);
   Task AddPlayerToLeague(Guid leagueId, PlayerEntity player);
   Task RemovePlayerFromHisLeague(PlayerEntity player);
}