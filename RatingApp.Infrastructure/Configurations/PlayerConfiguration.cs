using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RatingApp.Domain.Entities;

namespace RatingApp.Domain.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity>
{
   public void Configure(EntityTypeBuilder<PlayerEntity> builder)
   {
      builder.HasKey(p => p.Id);
      
      builder.Property(p => p.Nickname).IsRequired();
      builder.Property(p => p.Rating).IsRequired();
      
      builder
         .HasOne(p => p.League)
         .WithMany(l => l.Players)
         .HasForeignKey(p => p.LeagueId);
   }
}