using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RatingApp.Application.Interfaces;
using RatingApp.Infrastructure.Persistence;
using RatingApp.Infrastructure.Providers;
using RatingApp.Infrastructure.Repositories;
using RatingApp.Infrastructure.Services;

namespace RatingApp.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
   {
      services.AddDbContext<RatingAppDbContext>(options =>
         options.UseNpgsql(configuration.GetConnectionString("Default")));

      services.AddSingleton<IGuidProvider, SequentialGuidProvider>();
      
      services.AddScoped<ITransactionManager, TransactionManager>();
      services.AddScoped<IPlayerRepository, PlayerRepository>();
      services.AddScoped<ILeagueRepository, LeagueRepository>();

      return services;
   }
}