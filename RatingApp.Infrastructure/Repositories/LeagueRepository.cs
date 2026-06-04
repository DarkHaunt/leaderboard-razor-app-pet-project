using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace RatingApp.Infrastructure.Repositories;

public class LeagueRepository : ILeagueRepository
{
   public Task<LeagueEntity> GetByIdAsync(Guid id)
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<LeagueEntity>> GetAll(Func<LeagueEntity, bool>? predicate = null)
   {
      throw new NotImplementedException();
   }

   public Task AddAsync(LeagueEntity league)
   {
      throw new NotImplementedException();
   }

   public Task UpdateAsync(LeagueEntity league)
   {
      throw new NotImplementedException();
   }

   public Task DeleteAsync(LeagueEntity league)
   {
      throw new NotImplementedException();
   }
}