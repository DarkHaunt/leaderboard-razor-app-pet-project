using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Configurations;

public class LeagueConfiguration : IEntityTypeConfiguration<LeagueEntity>
{
   public void Configure(EntityTypeBuilder<LeagueEntity> builder)
   {
      builder.HasKey(l => l.Id);

      builder.Property(l => l.Name).IsRequired();
      builder.Property(l => l.Description).IsRequired();
      builder.Property(l => l.RequiredRating).IsRequired();
      
      builder.HasMany(l => l.Players)
         .WithOne(p => p.League)
         .HasForeignKey(p => p.LeagueId);
   }
}