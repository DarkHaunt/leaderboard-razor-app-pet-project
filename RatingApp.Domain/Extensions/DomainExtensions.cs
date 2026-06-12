using Microsoft.Extensions.DependencyInjection;
using RatingApp.Domain.Services;

namespace RatingApp.Domain.Extensions;

public static class DomainExtensions
{
   public static void AddDomainServices(this IServiceCollection services)
   {
      services.AddScoped<LeagueDeterminer>();
   }
}