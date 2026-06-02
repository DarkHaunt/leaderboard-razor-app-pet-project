using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface IPlayerRepository
{
   Task<PlayerEntity> GetByIdAsync(Guid id);
   Task<IEnumerable<PlayerEntity>> GetAll(Func<PlayerEntity, bool>? predicate = null);
   Task AddAsync(PlayerEntity player);
   Task UpdateAsync(PlayerEntity player);
   Task DeleteAsync(PlayerEntity player);
}