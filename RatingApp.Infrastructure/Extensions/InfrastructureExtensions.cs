using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RatingApp.Application.Interfaces;
using RatingApp.Infrastructure.Persistence;
using RatingApp.Infrastructure.Repositories;

namespace RatingApp.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddDbContext<RatingAppDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("Default")));

      services.AddScoped<IPlayerRepository, PlayerRepository>();

      return services;
   }
}