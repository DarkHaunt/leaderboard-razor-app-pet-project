using Microsoft.EntityFrameworkCore;
using RatingApp.Domain.Configurations;
using RatingApp.Domain.Entities;

namespace RatingApp.Infrastructure.Persistence;

public class RatingAppDbContext(DbContextOptions<RatingAppDbContext> options) : DbContext(options)
{
   public DbSet<PlayerEntity> Players { get; set; }
   public DbSet<LeagueEntity> Leagues { get; set; }
   
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.ApplyConfiguration(new PlayerConfiguration());
      modelBuilder.ApplyConfiguration(new LeagueConfiguration());
      
      base.OnModelCreating(modelBuilder);
   }
}