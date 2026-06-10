using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public sealed class PlayerRepository(RatingAppDbContext context) : IPlayerRepository
{
   public async Task<List<PlayerEntity>> GetAllPlayersAsync()
   {
      return await context.Players
         .AsNoTracking()
         .ToListAsync();
   }

   public async Task<PlayerEntity?> GetPlayerByIdAsync(Guid id)
   {
      return await context.Players
         .AsNoTracking()
         .FirstOrDefaultAsync(p => p.Id == id);
   }

   public async Task<PlayerEntity?> GetPlayerTrackedByIdAsync(Guid id) =>
      await context.Players.FirstOrDefaultAsync(p => p.Id == id);

   public async Task AddPlayerAsync(Guid id, string nickname, int rating)
   {
      var player = new PlayerEntity
      {
         Id = id,
         Nickname = nickname,
         Rating = rating
      };
      
      await context.Players.AddAsync(player);
      await context.SaveChangesAsync();
   }

   public async Task UpdatePlayerAsync(Guid id, string nickname, int rating)
   {
      await context.Players.Where(p => p.Id == id)
         .ExecuteUpdateAsync
         (
            b => b.SetProperty(p => p.Nickname, nickname)
                  .SetProperty(p => p.Rating, rating)
         );
   }

   public async Task DeletePlayerAsync(Guid id)
   {
      await context.Players
         .Where(p => p.Id == id)
         .ExecuteDeleteAsync();
   }

   public async Task UpdateLeagueForPlayer(Guid playerId, LeagueEntity? league)
   {
      PlayerEntity player = await GetPlayerByIdTrackedAsync(playerId);
      
      player.LeagueId = league?.Id ?? Guid.Empty;
      player.League = league;
      
      await context.SaveChangesAsync();
   }

   private async Task<PlayerEntity> GetPlayerByIdTrackedAsync(Guid id)
   {
      PlayerEntity? player = await GetPlayerTrackedByIdAsync(id);
      return player ?? throw new ArgumentNullException($"Player of id {id} not found");
   }
}