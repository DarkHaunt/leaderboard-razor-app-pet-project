using Microsoft.AspNetCore.Mvc.RazorPages;
using RatingApp.Application.Interfaces;

namespace leaderboard_razor_app_pet_project.Pages;

public class AdminPage(IPlayerRepository playerRepository, ILeagueRepository leagueRepository) : PageModel
{
   public void OnGet()
   {
      
   }
}