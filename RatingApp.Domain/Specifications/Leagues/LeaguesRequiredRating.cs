using System.Linq.Expressions;
using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Specifications.Leagues;

public sealed class LeaguesRequiredRating(uint requiredRating) : Specification<LeagueEntity>
{
   public override Expression<Func<LeagueEntity, bool>> ToExpression()
   {
      return x => x.RequiredRating > requiredRating;
   }
}