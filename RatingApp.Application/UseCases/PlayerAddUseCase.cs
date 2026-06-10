using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Services;

namespace RatingApp.Application.UseCases;

public class PlayerAddUseCase(
   IPlayerRepository playerRepository,
   ILeagueRepository leagueRepository,
   LeagueDeterminer leagueDeterminer,
   IGuidProvider guidProvider)
{
   public async Task CreatePlayerAsync(string nickname, int? rating = null)
   {
      var playerRating = rating ?? 0;
      Guid playerId = guidProvider.CreateNew();

      await playerRepository.AddPlayerAsync(playerId, nickname, playerRating);
      LeagueEntity? league = leagueDeterminer.DetermineLeagueForRating(playerRating, await leagueRepository.GetAllLLeaguesAsync());

      if (league != null)
         await AddPlayerToLeagueAsync(playerId, league.Id);
   }

   private async Task AddPlayerToLeagueAsync(Guid playerId, Guid leagueId)
   {
      PlayerEntity? player = await playerRepository.GetPlayerTrackedByIdAsync(playerId);

      if (player == null)
         throw new Exception($"Added player with id {playerId}, but could not find it in database");

      LeagueEntity? leagueTr = await leagueRepository.GetLeagueByIdTrackedAsync(leagueId);
         
      if (leagueTr == null)
         throw new Exception($"League with id {leagueId}, but could not find it in database");

      await playerRepository.UpdateLeagueForPlayer(player.Id, leagueTr);
      await leagueRepository.AddPlayerToLeague(leagueTr.Id, player);
   }
}