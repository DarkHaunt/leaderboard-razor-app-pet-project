using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Services;

public class LeagueDeterminer
{
   public LeagueEntity? DetermineLeagueForRating(int playerRating, IEnumerable<LeagueEntity> leagues)
      => leagues
         .Where(l => l.RequiredRating <= playerRating)
         .MaxBy(l => l.RequiredRating);
}