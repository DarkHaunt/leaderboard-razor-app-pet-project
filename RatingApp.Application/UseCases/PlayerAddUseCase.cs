using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Specifications.Leagues;

namespace RatingApp.Application.UseCases;

public class PlayerCreateUseCase(
   IPlayerRepository playerRepository,
   ILeagueRepository leagueRepository,
   IGuidProvider guidProvider)
{
   public async Task CreatePlayerAsync(string nickname, int? rating = null, CancellationToken ct = default)
   {
      var playerRating = rating ?? 0;
      Guid playerId = guidProvider.CreateNew();

      await playerRepository.AddPlayerAsync(playerId, nickname, playerRating, ct);
      var leaguesBelowRating = new LeaguesBelowRating((uint)playerRating);
      var leaguesPassedByPlayer = await leagueRepository.GetAllLeaguesAsync(leaguesBelowRating, orderBy: l => l.RequiredRating, ct: ct);
      LeagueEntity? league = leaguesPassedByPlayer.LastOrDefault();

      if (league != null)
         await AddPlayerToLeagueAsync(playerId, league.Id, ct);
   }

   private async Task AddPlayerToLeagueAsync(Guid playerId, Guid leagueId, CancellationToken ct = default)
   {
      PlayerEntity? player = await playerRepository.GetPlayerByIdAsync(playerId, tracked: true, ct);

      if (player == null)
         throw new Exception($"Added player with id {playerId}, but could not find it in database");

      LeagueEntity? leagueTr = await leagueRepository.GetLeagueByIdAsync(leagueId, tracked: true, ct);
         
      if (leagueTr == null)
         throw new Exception($"League with id {leagueId}, but could not find it in database");

      await playerRepository.UpdateLeagueForPlayer(player.Id, leagueTr, ct);
      await leagueRepository.AddPlayerToLeague(leagueTr.Id, player, ct);
   }
}