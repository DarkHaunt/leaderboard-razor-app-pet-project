using RatingApp.Domain.Entities;

namespace RatingApp.Application.Interfaces;

public interface IPlayerRepository
{
   Task<List<PlayerEntity>> GetAllAsync();
   Task<PlayerEntity?> GetByIdAsync(Guid id);
   Task AddAsync(string nickname, int? rating = null);
   Task UpdateAsync(Guid id, string nickname, int rating);
   Task DeleteAsync(Guid id);
}