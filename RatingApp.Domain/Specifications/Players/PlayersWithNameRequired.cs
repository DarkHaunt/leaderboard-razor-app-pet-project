using System.Linq.Expressions;
using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Specifications.Players;

public sealed class PlayersWithNameRequired(string nickname) : Specification<PlayerEntity>
{
   public override Expression<Func<PlayerEntity, bool>> ToExpression()
   {
      return x => x.Nickname == nickname;
   }
}