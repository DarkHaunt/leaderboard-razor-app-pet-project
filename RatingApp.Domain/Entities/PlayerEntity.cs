namespace RatingApp.Domain.Entities;

public sealed class PlayerEntity
{
   public Guid Id { get; set; }
   public required string Nickname { get; set; }
   public int Rating { get; set; }
   public Guid? LeagueId { get; set; }
   public LeagueEntity? League { get; set; }
}