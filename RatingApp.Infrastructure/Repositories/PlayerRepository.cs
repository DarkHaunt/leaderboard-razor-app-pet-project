using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public sealed class PlayerRepository(RatingAppDbContext context) : IPlayerRepository
{
   public async Task<List<PlayerEntity>> GetAllPlayersAsync(Specification<PlayerEntity>? specification = null,
      Expression<Func<PlayerEntity, object>>? orderBy = null, CancellationToken ct = default)
   {
      var q = context.Players.AsNoTracking();

      if (specification is not null)
         q = q.Where(specification.ToExpression());

      if (orderBy is not null)
         q = q.OrderBy(orderBy);

      return await q
         .Include(p => p.League)
         .ToListAsync(ct);
   }

   public async Task<PlayerEntity?> GetPlayerByIdAsync(Guid id, bool tracked = false, CancellationToken ct = default)
   {
      var q = context.Players.Include(p => p.League);

      if (tracked)
      {
         return await q
            .FirstOrDefaultAsync(p => p.Id == id, ct);
      }

      return await q
         .AsNoTracking()
         .FirstOrDefaultAsync(p => p.Id == id, ct);

   }

   public async Task AddPlayerAsync(Guid id, string nickname, int rating, CancellationToken ct = default)
   {
      var player = new PlayerEntity
      {
         Id = id,
         Nickname = nickname,
         Rating = rating
      };
      
      await context.Players.AddAsync(player, ct);
      await context.SaveChangesAsync(ct);
   }

   public async Task UpdatePlayerAsync(Guid id, string nickname, int rating, CancellationToken ct = default)
   {
      await context.Players.Where(p => p.Id == id)
         .ExecuteUpdateAsync
         (
            b => b
               .SetProperty(p => p.Nickname, nickname)
               .SetProperty(p => p.Rating, rating),
            ct
         );
   }

   public async Task DeletePlayerAsync(Guid id, CancellationToken ct = default)
   {
      await context.Players
         .Where(p => p.Id == id)
         .ExecuteDeleteAsync(ct);
   }

   public async Task UpdateLeagueForPlayer(Guid playerId, LeagueEntity? league, CancellationToken ct = default)
   {
      PlayerEntity player = await GetPlayerGuaranteedTrackedAsync(playerId, ct);
      
      player.LeagueId = league?.Id ?? Guid.Empty;
      player.League = league;
      
      await context.SaveChangesAsync(ct);
   }

   private async Task<PlayerEntity> GetPlayerGuaranteedTrackedAsync(Guid id, CancellationToken ct = default)
   {
      PlayerEntity? player = await GetPlayerByIdAsync(id, tracked: true, ct);
      return player ?? throw new ArgumentNullException($"Player of id {id} not found");
   }
}