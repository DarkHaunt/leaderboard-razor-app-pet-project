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
      
      var leaguesBelowRating = new LeaguesBelowRating((uint)playerRating);
      var leaguesPassedByPlayer = await leagueRepository.GetAllLeaguesAsync(leaguesBelowRating, orderBy: l => l.RequiredRating, ct: ct);
      LeagueEntity? league = leaguesPassedByPlayer.LastOrDefault();

      await playerRepository.AddPlayerAsync(playerId, nickname, playerRating, league?.Id, ct);
      return await transactionManager.CommitTransactionAsync(ct);
   }
}