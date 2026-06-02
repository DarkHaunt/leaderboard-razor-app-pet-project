using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace RatingApp.Infrastructure.Repositories;

public sealed class PlayerRepository : IPlayerRepository
{
   public Task<PlayerEntity> GetByIdAsync(Guid id)
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<PlayerEntity>> GetAll(Func<PlayerEntity, bool>? predicate = null)
   {
      throw new NotImplementedException();
   }

   public Task AddAsync(PlayerEntity player)
   {
      throw new NotImplementedException();
   }

   public Task UpdateAsync(PlayerEntity player)
   {
      throw new NotImplementedException();
   }

   public Task DeleteAsync(PlayerEntity player)
   {
      throw new NotImplementedException();
   }
}