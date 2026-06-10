using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public sealed class PlayerRepository(RatingAppDbContext context, IGuidProvider guidProvider) : IPlayerRepository
{
   public async Task<List<PlayerEntity>> GetAllAsync()
   {
      return await context.Players
         .AsNoTracking()
         .ToListAsync();
   }

   public async Task<PlayerEntity?> GetByIdAsync(Guid id)
   {
      return await context.Players
         .AsNoTracking()
         .FirstOrDefaultAsync(p => p.Id == id);
   }

   public async Task AddAsync(string nickname, int? rating = null)
   {
      var player = new PlayerEntity
      {
         Id = guidProvider.CreateNew(),
         Nickname = nickname,
         Rating = rating ?? 0
      };
      
      await context.Players.AddAsync(player);
      await context.SaveChangesAsync();
   }

   public async Task UpdateAsync(Guid id, string nickname, int rating)
   {
      await context.Players.Where(p => p.Id == id)
         .ExecuteUpdateAsync
         (
            b => b.SetProperty(p => p.Nickname, nickname)
                  .SetProperty(p => p.Rating, rating)
         );
   }

   public async Task DeleteAsync(Guid id)
   {
      await context.Players
         .Where(p => p.Id == id)
         .ExecuteDeleteAsync();
   }
}