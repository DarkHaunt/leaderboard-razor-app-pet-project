using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface ILeagueRepository
{
   Task<List<LeagueEntity>> GetAllAsync();
   Task<LeagueEntity?> GetByIdAsync(Guid id);
   Task AddAsync(string name, string description, int requiredRating);
   Task UpdateAsync(Guid id, string name, string description, int requiredRating);
   Task DeleteAsync(Guid id);
   Task AddPlayerToLeagueAsync(Guid leagueId, PlayerEntity player);
   Task RemovePlayerFromLeagueAsync(Guid leagueId, PlayerEntity player);
}