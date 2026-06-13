using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;
using RatingApp.Domain.Entities;

namespace leaderboard_razor_app_pet_project.Pages;

public class IndexModel(ILeagueRepository leagueRepository) : PageModel
{
   public List<LeagueEntity> Leagues { get; set; } = new();
   
   public async Task OnGetAsync()
   {
      Leagues = await leagueRepository.GetAllLeaguesAsync(orderBy: l => l.RequiredRating, ct: HttpContext.RequestAborted);
   }
}