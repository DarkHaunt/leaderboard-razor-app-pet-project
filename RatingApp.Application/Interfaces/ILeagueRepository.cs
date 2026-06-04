using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface ILeagueRepository
{
   Task<LeagueEntity> GetByIdAsync(Guid id);
   Task<IEnumerable<LeagueEntity>> GetAll(Func<LeagueEntity, bool>? predicate = null);
   Task AddAsync(LeagueEntity league);
   Task UpdateAsync(LeagueEntity league);
   Task DeleteAsync(LeagueEntity league);
}