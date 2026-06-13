namespace RatingApp.Domain.Entities;

public sealed class LeagueEntity
{
   public Guid Id { get; set; }
   public required string Name { get; set; }
   public required string Description { get; set; }
   public required uint RequiredRating { get; set; }
   public List<PlayerEntity> Players { get; set; } = [];
}