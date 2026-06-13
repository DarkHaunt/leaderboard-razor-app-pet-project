using System.Linq.Expressions;
using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Specifications.Leagues;

public sealed class LeaguesBelowRating(uint rating) : Specification<LeagueEntity>
{
   public override Expression<Func<LeagueEntity, bool>> ToExpression()
   {
      return x => x.RequiredRating < rating;
   }
}