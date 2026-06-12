using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public class LeagueRepository(RatingAppDbContext context) : ILeagueRepository
{
   public async Task<List<LeagueEntity>> GetAllLLeaguesAsync()
   {
      return await context.Leagues
         .AsNoTracking()
         .ToListAsync();
   }

   public async Task<LeagueEntity?> GetLeagueByIdAsync(Guid id)
   {
      return await context.Leagues
         .AsNoTracking()
         .FirstOrDefaultAsync(l => l.Id == id);
   }

   public async Task<LeagueEntity?> GetLeagueByIdTrackedAsync(Guid id) =>
      await context.Leagues.FirstOrDefaultAsync(l => l.Id == id);

   public async Task AddLeagueAsync(Guid id, string name, string description, int requiredRating)
   {
      var league = new LeagueEntity
      {
         Id = id,
         Description = description,
         Name = name,
         RequiredRating = requiredRating
      };

      await context.Leagues.AddAsync(league);
      await context.SaveChangesAsync();
   }

   public async Task UpdateLeagueAsync(Guid id, string name, string description, int requiredRating)
   {
      await context.Leagues.Where(l => l.Id == id)
         .ExecuteUpdateAsync
         (
            b => b.SetProperty(l => l.Name, name)
                  .SetProperty(l => l.Description, description)
                  .SetProperty(l => l.RequiredRating, requiredRating)
         );
   }

   public async Task DeleteLeagueAsync(Guid id)
   {
      await context.Leagues
         .Where(l => l.Id == id)
         .ExecuteDeleteAsync();
   }

   public async Task AddPlayerToLeague(Guid leagueId, PlayerEntity player)
   {
      LeagueEntity league = await GetLeagueTrackedAsync(leagueId);
      league.Players.Add(player);

      await context.SaveChangesAsync();
   }

   public async Task RemovePlayerFromHisLeague(PlayerEntity player)
   {
      if(player.LeagueId == null) return;
      
      LeagueEntity league = await GetLeagueTrackedAsync(player.LeagueId.Value);
      league.Players.Remove(player);
      
      await context.SaveChangesAsync();
   }

   private async Task<LeagueEntity> GetLeagueTrackedAsync(Guid id)
   {
      LeagueEntity? league = await GetLeagueByIdTrackedAsync(id);
      return league ?? throw new ArgumentNullException($"League of id {id} not found");
   }
}