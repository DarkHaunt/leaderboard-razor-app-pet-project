using RatingApp.Domain.Enums;

namespace RatingApp.Domain.Entities;

public sealed class PlayerEntity
{
   public Guid Id { get; set; }
   public required string Nick { get; set; }
   public required string Country { get; set; }
   public int Level { get; set; }
   public int Rating { get; set; }
   public PlayerLeague League { get; set; }
}