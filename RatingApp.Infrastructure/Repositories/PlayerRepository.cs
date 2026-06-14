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

   public async Task<PlayerEntity?> GetPlayerByIdAsync(Guid id, CancellationToken ct = default)
   {
      return await context.Players.AsNoTracking()
         .Include(p => p.League)
         .FirstOrDefaultAsync(l => l.Id == id, ct);
   }

   public async Task AddPlayerAsync(Guid id, string nickname, int rating, Guid? leagueId, CancellationToken ct = default)
   {
      var player = new PlayerEntity
      {
         Id = id,
         Nickname = nickname,
         Rating = rating,
         LeagueId = leagueId
      };
      
      await context.Players.AddAsync(player, ct);
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
}