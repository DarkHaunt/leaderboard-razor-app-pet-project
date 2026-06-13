using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public class LeagueRepository(RatingAppDbContext context) : ILeagueRepository
{
   public async Task<List<LeagueEntity>> GetAllLeaguesAsync(Specification<LeagueEntity>? specification = null,
      Expression<Func<LeagueEntity, object>>? orderBy = null, CancellationToken ct = default)
   {
      var q = context.Leagues.AsNoTracking();

      if (specification is not null)
         q = q.Where(specification.ToExpression());

      if (orderBy is not null)
         q = q.OrderBy(orderBy);

      return await q
         .Include(l => l.Players)
         .ToListAsync(ct);
   }

   public async Task<LeagueEntity?> GetLeagueByIdAsync(Guid id, bool tracked = false, CancellationToken ct = default)
   {
      var q = context.Leagues.Include(l => l.Players);

      if (tracked)
      {
         return await q
            .FirstOrDefaultAsync(l => l.Id == id, ct);
      }

      return await q
         .AsNoTracking()
         .FirstOrDefaultAsync(l => l.Id == id, ct);
   }

   public async Task AddLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default)
   {
      var league = new LeagueEntity
      {
         Id = id,
         Description = description,
         Name = name,
         RequiredRating = requiredRating
      };

      await context.Leagues.AddAsync(league, ct);
      await context.SaveChangesAsync(ct);
   }

   public async Task UpdateLeagueAsync(Guid id, string name, string description, uint requiredRating, CancellationToken ct = default)
   {
      await context.Leagues.Where(l => l.Id == id)
         .ExecuteUpdateAsync
         (
            b => b
               .SetProperty(l => l.Name, name)
               .SetProperty(l => l.Description, description)
               .SetProperty(l => l.RequiredRating, requiredRating), 
            ct
         );
   }

   public async Task DeleteLeagueAsync(Guid id, CancellationToken ct = default)
   {
      await context.Leagues
         .Where(l => l.Id == id)
         .ExecuteDeleteAsync(ct);
   }

   public async Task AddPlayerToLeague(Guid leagueId, PlayerEntity player, CancellationToken ct = default)
   {
      LeagueEntity league = await GetLeagueGuaranteedTrackedAsync(leagueId, ct);
      league.Players.Add(player);

      await context.SaveChangesAsync(ct);
   }

   public async Task RemovePlayerFromHisLeague(PlayerEntity player, CancellationToken ct = default)
   {
      if (player.LeagueId == null) return;

      LeagueEntity league = await GetLeagueGuaranteedTrackedAsync(player.LeagueId.Value, ct);
      league.Players.Remove(player);

      await context.SaveChangesAsync(ct);
   }

   private async Task<LeagueEntity> GetLeagueGuaranteedTrackedAsync(Guid id, CancellationToken ct = default)
   {
      LeagueEntity? league = await GetLeagueByIdAsync(id, tracked: true, ct);
      return league ?? throw new ArgumentNullException($"League of id {id} not found");
   }
}