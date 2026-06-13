using RatingApp.Application.Interfaces;
using RatingApp.Domain.Specifications.Players;

namespace RatingApp.Application.UseCases;

public class PlayerDeleteUseCase(
   IPlayerRepository playerRepository,
   ILeagueRepository leagueRepository)
{
   public async Task DeleteAllPlayersWithNicknameAsync(string nickname, CancellationToken ct = default)
   {
      var nicknameSpecification = new PlayersWithName(nickname);
      var players = await playerRepository.GetAllPlayersAsync(nicknameSpecification, ct: ct);

      await Parallel.ForEachAsync(players, ct, async (player, token) =>
      {
         if (player.League != null)
            await leagueRepository.RemovePlayerFromHisLeague(player, token);

         await playerRepository.DeletePlayerAsync(player.Id, token);
      });
   }
}