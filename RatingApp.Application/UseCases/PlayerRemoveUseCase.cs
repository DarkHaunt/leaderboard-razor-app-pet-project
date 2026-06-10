using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace RatingApp.Application.UseCases;

public class PlayerRemoveUseCase(
   IPlayerRepository playerRepository,
   ILeagueRepository leagueRepository)
{
   public async Task DeletePlayerAsync(Guid playerId)
   {
      PlayerEntity? player = await playerRepository.GetPlayerTrackedByIdAsync(playerId);
      
      if(player == null) return;

      if (player.League != null)
         await leagueRepository.RemovePlayerFromHisLeague(player);
      
      await playerRepository.DeletePlayerAsync(playerId);
   }
}