using CSharpFunctionalExtensions;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;
using RatingApp.Domain.Errors;
using RatingApp.Domain.Specifications.Leagues;

namespace RatingApp.Application.UseCases;

public class PlayerCreateUseCase(
   IPlayerRepository playerRepository,
   ILeagueRepository leagueRepository,
   ITransactionManager transactionManager,
   IGuidProvider guidProvider)
{
   public async Task<UnitResult<Error>> CreatePlayerAsync(string nickname, int? rating = null, CancellationToken ct = default)
   {
      var beginTransactionResult = await transactionManager.BeginTransactionAsync(ct);

      if (beginTransactionResult.IsFailure)
         return beginTransactionResult;

      var playerRating = rating ?? 0;
      Guid playerId = guidProvider.CreateNew();

      await playerRepository.AddPlayerAsync(playerId, nickname, playerRating, ct);

      var leaguesBelowRating = new LeaguesBelowRating((uint)playerRating);
      var leaguesPassedByPlayer = await leagueRepository.GetAllLeaguesAsync(leaguesBelowRating, orderBy: l => l.RequiredRating, ct: ct);
      LeagueEntity? league = leaguesPassedByPlayer.LastOrDefault();

      if (league != null)
      {
         var addPlayerToLeagueResult = await AddPlayerToLeagueAsync(playerId, league.Id, ct);

         if (addPlayerToLeagueResult.IsFailure)
         {
            await transactionManager.RollbackTransaction(ct);
            return addPlayerToLeagueResult;
         }
      }

      return await transactionManager.CommitTransactionAsync(ct);
   }

   private async Task<UnitResult<Error>> AddPlayerToLeagueAsync(Guid playerId, Guid leagueId, CancellationToken ct = default)
   {
      try
      {
         await playerRepository.AddPlayerToLeagueAsync(playerId, leagueId, ct);
         return UnitResult.Success<Error>();
      }
      catch (OperationCanceledException)
      {
         return GenericErrors.OperationCanceledError();
      }
      catch (Exception e)
      {
         return GenericErrors.DBError(e.Message);
      }
   }
}