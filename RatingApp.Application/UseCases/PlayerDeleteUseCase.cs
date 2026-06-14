using CSharpFunctionalExtensions;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Errors;
using RatingApp.Domain.Specifications.Players;

namespace RatingApp.Application.UseCases;

public class PlayerDeleteUseCase(
   IPlayerRepository playerRepository,
   ITransactionManager transactionManager)
{
   public async Task<UnitResult<Error>> DeleteAllPlayersWithNicknameAsync(string nickname, CancellationToken ct = default)
   {
      var transactionBeginResult = await transactionManager.BeginTransactionAsync(ct);

      if (transactionBeginResult.IsFailure)
         return transactionBeginResult;

      var nicknameSpecification = new PlayersWithName(nickname);
      var players = await playerRepository.GetAllPlayersAsync(nicknameSpecification, ct: ct);
      
      foreach (PlayerEntity player in players)
         await playerRepository.DeletePlayerAsync(player.Id, ct);
      
      return await transactionManager.CommitTransactionAsync(ct);
   }
}