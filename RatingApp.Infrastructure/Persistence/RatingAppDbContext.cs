using Microsoft.EntityFrameworkCore;
using RatingApp.Domain.Entities;

namespace RatingApp.Infrastructure.Persistence;

public class RatingAppDbContext : DbContext
{
   public DbSet<PlayerEntity> Players { get; set; }
   public DbSet<LeagueEntity> Leagues { get; set; }
}