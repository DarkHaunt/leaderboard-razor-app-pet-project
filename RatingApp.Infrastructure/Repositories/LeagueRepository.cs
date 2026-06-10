using Microsoft.EntityFrameworkCore;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Infrastructure.Persistence;

namespace RatingApp.Infrastructure.Repositories;

public class LeagueRepository(RatingAppDbContext context, IGuidProvider guidProvider) : ILeagueRepository
{
   public async Task<List<LeagueEntity>> GetAllAsync()
   {
      return await context.Leagues
         .AsNoTracking()
         .ToListAsync();
   }

   public async Task<LeagueEntity?> GetByIdAsync(Guid id)
   {
      return await context.Leagues
         .AsNoTracking()
         .FirstOrDefaultAsync(l => l.Id == id);
   }

   public async Task AddAsync(string name, string description, int requiredRating)
   {
      var league = new LeagueEntity
      {
         Id = guidProvider.CreateNew(),
         Description = description,
         Name = name,
         RequiredRating = requiredRating
      };

      await context.Leagues.AddAsync(league);
      await context.SaveChangesAsync();
   }

   public async Task UpdateAsync(Guid id, string name, string description, int requiredRating)
   {
      await context.Leagues.Where(l => l.Id == id)
         .ExecuteUpdateAsync
         (
            b => b.SetProperty(l => l.Name, name)
                  .SetProperty(l => l.Description, description)
                  .SetProperty(l => l.RequiredRating, requiredRating)
         );
   }

   public async Task DeleteAsync(Guid id)
   {
      await context.Leagues
         .Where(l => l.Id == id)
         .ExecuteDeleteAsync();
   }

   public async Task AddPlayerToLeagueAsync(Guid leagueId, PlayerEntity player)
   {
      LeagueEntity league = await GetLeagueTrackedAsync(leagueId);
      
      league.Players.Add(player);
      player.LeagueId = leagueId;
      player.League = league;
      
      await context.SaveChangesAsync();
   }

   public async Task RemovePlayerFromLeagueAsync(Guid leagueId, PlayerEntity player)
   {
      LeagueEntity league = await GetLeagueTrackedAsync(leagueId);
      
      league.Players.Remove(player);
      player.LeagueId = Guid.Empty;
      player.League = null;
      
      await context.SaveChangesAsync();
   }

   private async Task<LeagueEntity> GetLeagueTrackedAsync(Guid id)
   {
      LeagueEntity? league = await context.Leagues.FirstOrDefaultAsync(l => l.Id == id);
      return league ?? throw new ArgumentNullException($"League of id {id} not found");
   }
}