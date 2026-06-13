using Microsoft.Extensions.DependencyInjection;
using RatingApp.Application.UseCases;

namespace RatingApp.Application.Extensions;

public static class ApplicationExtensions
{
   public static void AddApplicationServices(this IServiceCollection services)
   {
      services.AddScoped<PlayerCreateUseCase>();
      services.AddScoped<PlayerDeleteUseCase>();
   }
}